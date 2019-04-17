using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace SharedAccessKey
{
    public static class SharedAccessKeyApp
    {
        [FunctionName("SharedAccessKeyApp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            var config = BuildAppConfig(context);

            log.LogInformation("data");
            log.LogInformation(config["STORAGE_CONNECTION_STRING"]);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        private static IConfigurationRoot BuildAppConfig(ExecutionContext context) {

                var config = new ConfigurationBuilder().SetBasePath
                (context.FunctionAppDirectory).AddJsonFile("local.settings.json", 
                optional : true, reloadOnChange : true).AddEnvironmentVariables().Build();
                return config;
        }
    }
}
