using AzCore.Shared;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ClaimTicketService.Tasks
{
    public class ArchiveImageTask
    {
        [FunctionName(TasksConstants.ArchiveImageTask)]
        public static string SayHello([ActivityTrigger] ClaimForm claim, ILogger log)
        {
            log.LogInformation($"Saying hello to {claim.Name}.");
            return $"Validate {claim.Name}!";
        }
    }
}
