using AzCore.Shared;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ClaimTicketService.Tasks
{
    public class GetImageVisionTask
    {
        [FunctionName(TasksConstants.GetImageVisionTask)]
        public static string SayHello([ActivityTrigger] ClaimForm claim, ILogger log)
        {
            log.LogInformation($"Saying hello to {claim.Name}.");
            return $"Validate {claim.Name}!";
        }
    }
}
