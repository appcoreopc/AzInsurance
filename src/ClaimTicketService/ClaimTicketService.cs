using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace ClaimTicketService
{
    public static class ClaimTicketServiceFunction
    {
        [FunctionName(TasksConstants.ClaimTicketServiceFunctionTask)]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            // outputs.Add(await context.CallActivityAsync<string>(TasksConstants.ValidateTask, "Tokyo"));
            outputs.Add(await context.CallActivityAsync<string>(TasksConstants.ValidateTask, "Seattle"));
            outputs.Add(await context.CallActivityAsync<string>(TasksConstants.ValidateTask, "London"));
            return outputs;
        }

        [FunctionName("ClaimTicketServiceFunction_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]HttpRequestMessage req,
            [OrchestrationClient]DurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync(TasksConstants.ClaimTicketServiceFunctionTask, null);
            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");
            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
