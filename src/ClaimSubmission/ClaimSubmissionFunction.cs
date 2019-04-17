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

        [FunctionName("ClaimSubmissionFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            var config = BuildAppConfig(context);
            var targetConnectionString = config[StorageConnectionString];
           
            string name = req.Query["name"];

            var userClaimFormData = GetClaimDataInput<ClaimForm>(req.Body, "", "");
            var sender = new MessageSender(targetConnectionString, TargetClaimQueueName);
            await sender.SendAsync(CreateMessage("Invoice"));

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult(ClaimSubmissionSuccessful)
                : new BadRequestObjectResult(ErroSubmittingClaimMessage);
        }

        private static IConfigurationRoot BuildAppConfig(ExecutionContext context)
        {
            var config = new ConfigurationBuilder().SetBasePath
            (context.FunctionAppDirectory).AddJsonFile("local.settings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();
            return config;
        }

        public static Message CreateMessage(string label)
        {
            var msg = new Message(Encoding.UTF8.GetBytes("This is the body of message \"" + label + "\"."));
            msg.UserProperties.Add("Priority", 1);
            msg.UserProperties.Add("Importance", "High");
            msg.Label = "Invoice";
            msg.TimeToLive = TimeSpan.FromSeconds(90);
            return msg;
        }

        private static async Task<T> GetClaimDataInput<T>(Stream body, params string[] fields)
        {
            string requestBody = await new StreamReader(body).ReadToEndAsync();
            T data = JsonConvert.DeserializeObject<T>(requestBody);
            return data;
        }
    }
}
