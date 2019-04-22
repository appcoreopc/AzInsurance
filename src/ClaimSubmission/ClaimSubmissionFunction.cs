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
using AzCore.Shared;
using AzCore.Shared.JsonSerializer;

namespace ClaimSubmission
{
    public static class ClaimSubmissionFunction
    {
        private const string ErroSubmittingClaimMessage = "Error submmitting claim.";
        private const string ClaimSubmissionSuccessful = "Submission successful";

        [FunctionName("ClaimSubmissionFunction")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            var config = CoreConfiguration.BuildAppConfig(context);
            var messageConnectionString = config[ApplicationConstants.StorageConnectionString];

            if (messageConnectionString == null)
            {
                return new BadRequestObjectResult(ErroSubmittingClaimMessage);
            }

            var userClaimFormData = await MessageConverter.Deserialize<ClaimForm>(req.Body);
            var sender = new MessageSender(messageConnectionString, ApplicationConstants.TargetClaimQueueName);
            await sender.SendAsync(CreateMessage(userClaimFormData));

            return userClaimFormData != null
                ? (ActionResult)new OkObjectResult(ClaimSubmissionSuccessful)
                : new BadRequestObjectResult(ErroSubmittingClaimMessage);
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
    }
}
