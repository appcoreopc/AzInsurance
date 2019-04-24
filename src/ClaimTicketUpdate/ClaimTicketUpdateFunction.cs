using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AzCore.Shared;
using AzCore.Shared.JsonSerializer;
using AzCore.Shared.Claim;

namespace ClaimTicketUpdate
{
    public static class ClaimTicketUpdateFunction
    {
        [FunctionName("ClaimTicketUpdateFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            log.LogInformation("Claim ticket update");

            var config = CoreConfiguration.BuildAppConfig(context);
            var connectionString = config[ApplicationConstants.StorageConnectionString];

            if (connectionString == null)
            {
                return new BadRequestObjectResult($"Unable to connect to data store {connectionString}");
            }

            var userClaimFormData = await MessageConverter.Deserialize<ClaimForm>(req.Body);
            var processor = new ClaimProcessor(connectionString, ApplicationConstants.TableClaimName);
            await processor.UpdateClaimAsync(userClaimFormData);
            return (ActionResult)new OkObjectResult($"Hello");
        }
    }
}
