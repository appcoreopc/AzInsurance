using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ClaimTicketService.Tasks
{
    public class ValidateTask
    {
        [FunctionName(TasksConstants.ValidateTask)]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Saying hello to {name}.");
            return $"Validate {name}!";
        }
    }
}
