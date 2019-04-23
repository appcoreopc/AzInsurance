using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using AzCore.Shared;
using System;
using AzCore.Shared.JsonSerializer;

namespace ClaimValidation
{
    public static class ClaimValidationFunction
    {
        [FunctionName("ClaimValidationFunction")]
        public static void Run([ServiceBusTrigger(ApplicationConstants.TargetClaimQueueName, Connection = "MyConn")] string queueItem,
        Int32 deliveryCount, DateTime enqueuedTimeUtc, string messageId, ILogger log, ExecutionContext context)
        {
            log.LogInformation($"Claim validation content: {queueItem}");
            var config = CoreConfiguration.BuildAppConfig(context);
            var messageConnectionString = config[ApplicationConstants.StorageConnectionString];
            var userClaimFormData = MessageConverter.Deserialize<ClaimForm>(queueItem);
            ProcessMessage(userClaimFormData);
        }

        private static void ProcessMessage(ClaimForm data)
        {
            // Query database 
            
        }
    }
}
