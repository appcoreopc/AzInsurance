using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using AzCore.Shared;
using System;
using AzCore.Shared.JsonSerializer;
using AzCore.Shared.ClaimValidation;

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
            var connectionString = config[ApplicationConstants.StorageConnectionString];
            var userClaimFormData = MessageConverter.Deserialize<ClaimForm>(queueItem);
            ProcessMessage(userClaimFormData, connectionString, "claimTable");
        }

        private static void ProcessMessage(ClaimForm data, string connectionString, string tableName)
        {
            var processor = new ClaimValidationProcessor(connectionString, tableName);
            var record = processor.ProcessAsync(data);
            if (record != null)
            {
                // Place back into a processed queue
            }
        }
    }
}
