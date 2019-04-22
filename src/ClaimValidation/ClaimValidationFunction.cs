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

namespace ClaimValidation
{
    public static class ClaimValidationFunction
    {
        [FunctionName("ClaimValidationFunction")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        ILogger log, ExecutionContext context)
        {
            log.LogInformation($"Fake claim validation");
            await ValidateClaimData();
            return (ActionResult) new OkObjectResult("Ok");          
        }

        private static async Task ValidateClaimData() {

            await Task.Delay(1000);
        }
    }
}
