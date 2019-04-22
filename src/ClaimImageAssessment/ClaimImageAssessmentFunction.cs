using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ClaimImageAssessment
{
    public static class ClaimImageAssessmentFunction
    {
        private const string SettingFilename = "local.settings.json";
        private const string SubscriptionKey = "AZURE_VISION_SUBSCRIPTIONKEY";
        private const string VisionEndpoint = "AZURE_VISON_ENDPOINT";

        // Specify the features to return
        private static readonly List<VisualFeatureTypes> features =
            new List<VisualFeatureTypes>()
        {
            VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
            VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
            VisualFeatureTypes.Tags
        };

        [FunctionName("ClaimImageAssessmentFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            log.LogInformation("Analyzing image");

            var config = BuildAppConfig(context);
            var visionSubscriptionKey = config[SubscriptionKey];
            var endpoint = config[VisionEndpoint];

            var visionClient = SetupVisionClient(visionSubscriptionKey, endpoint);
            var imageData = await GetImageDataInput<ClaimImageAnalysis>(req.Body);
            var imageAnalysis = await AnalyzeRemoteAsync(visionClient, imageData.ImageSource, log);

            return imageAnalysis != null ? (ActionResult)new OkObjectResult($"Done analyzing image") : new BadRequestObjectResult("Unable to analyze image");
        }

        private static ComputerVisionClient SetupVisionClient(string visionSubscriptionKey, string endpoint)
        {
            var visionClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(visionSubscriptionKey), new System.Net.Http.DelegatingHandler[] { });
            visionClient.Endpoint = endpoint;
            return visionClient;
        }

        // Analyze a remote image
        private static async Task<ImageAnalysis> AnalyzeRemoteAsync(
            ComputerVisionClient computerVision, string imageUrl, ILogger log)
        {
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                log.LogInformation("\nInvalid remoteImageUrl:\n{0} \n", imageUrl);
                return null;
            }
            return await computerVision.AnalyzeImageAsync(imageUrl, features);
        }

        private static IConfigurationRoot BuildAppConfig(ExecutionContext context)
        {
            var config = new ConfigurationBuilder().SetBasePath
            (context.FunctionAppDirectory).AddJsonFile(SettingFilename, optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();
            return config;
        }

        private static async Task<T> GetImageDataInput<T>(Stream body)
        {
            string requestBody = await new StreamReader(body).ReadToEndAsync();
            T data = JsonConvert.DeserializeObject<T>(requestBody);
            return data;
        }
    }
}

