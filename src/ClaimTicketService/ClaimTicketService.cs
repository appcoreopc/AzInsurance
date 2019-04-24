using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AzCore.Shared;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace ClaimTicketService
{
    public static class ClaimTicketServiceFunction
    {
        [FunctionName("ClaimTicketServiceFunctionTask")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var outputs = new List<string>();
            outputs.Add(await context.CallActivityAsync<string>(TasksConstants.ValidateTask, new ClaimForm() {  Name = "Test" }));
            outputs.Add(await context.CallActivityAsync<string>(TasksConstants.ValidateTask, new ClaimForm() { Name = "Test2" }));
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
