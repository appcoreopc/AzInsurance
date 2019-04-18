using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ClaimSubmission
{
    public static class ClaimSubmissionFunction
    {
        private const string StorageConnectionString = "STORAGE_CONNECTION_STRING";
        private const string TargetClaimQueueName = "ClaimQueue";
        private const string ErroSubmittingClaimMessage = "Error submmitting claim.";
        private const string ClaimSubmissionSuccessful = "Submission successful";
        private const string SettingFilename = "local.settings.json";

        [FunctionName("ClaimSubmissionFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            var config = BuildAppConfig(context);
            var targetConnectionString = config[StorageConnectionString];

            if (targetConnectionString == null)
            {
                return new BadRequestObjectResult(ErroSubmittingClaimMessage);
            }

            var userClaimFormData = await GetClaimDataInput<ClaimForm>(req.Body);
            var sender = new MessageSender(targetConnectionString, TargetClaimQueueName);
            await sender.SendAsync(CreateMessage(userClaimFormData));

            return userClaimFormData != null
                ? (ActionResult)new OkObjectResult(ClaimSubmissionSuccessful)
                : new BadRequestObjectResult(ErroSubmittingClaimMessage);
        }

        private static IConfigurationRoot BuildAppConfig(ExecutionContext context)
        {
            var config = new ConfigurationBuilder().SetBasePath
            (context.FunctionAppDirectory).AddJsonFile(SettingFilename, optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();
            return config;
        }

        public static Message CreateMessage(ClaimForm message)
        {
            if (message == null)
            {
                return null;
            }

            var jsonContent = JsonConvert.SerializeObject(message);
            var msg = new Message(Encoding.UTF8.GetBytes(jsonContent));
            msg.ContentType = "application/json";
            msg.Label = message.Label;
            msg.TimeToLive = TimeSpan.FromSeconds(90);
            return msg;
        }

        private static async Task<T> GetClaimDataInput<T>(Stream body)
        {
            string requestBody = await new StreamReader(body).ReadToEndAsync();
            T data = JsonConvert.DeserializeObject<T>(requestBody);
            return data;
        }
    }
}
