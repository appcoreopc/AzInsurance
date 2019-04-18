using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace SharedAccessKey
{
    public static class SharedAccessKeyApp
    {
        private const string UnableObtainedSharedAccessKey = "Unable to obtained shared access key.";
        private const string FilenameTargetField = "filename";
        private const string StorageConnectionString = "STORAGE_CONNECTION_STRING";
        private const string TargetInvoiceStorageContainer = "invoice";
        private const string LocalSettingFile = "local.settings.json";

        [FunctionName("SharedAccessKeyApp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            try
            {
                var config = BuildAppConfig(context);
                string name = req.Query[FilenameTargetField];
                var targetConnectionString = config[StorageConnectionString];
                var sharedAccessKey = await GetSharedAccessKeyAsync(targetConnectionString, name, TargetInvoiceStorageContainer);

                return !string.IsNullOrEmpty(sharedAccessKey) ? (ActionResult)new OkObjectResult(sharedAccessKey)
                  : new BadRequestObjectResult(UnableObtainedSharedAccessKey);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }

            return (ActionResult)new OkObjectResult(UnableObtainedSharedAccessKey);
        }

        private static async Task<string> GetSharedAccessKeyAsync(string storageConnectionString, string targetfilename, string targetcontainer)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var client = storageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(targetcontainer);
            await container.CreateIfNotExistsAsync();

            CloudBlockBlob blob = container.GetBlockBlobReference(targetfilename);

            SharedAccessBlobPolicy adHocSAS = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(5),
                Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Create
            };

            var sasBlobToken = blob.GetSharedAccessSignature(adHocSAS);
            return blob.Uri + sasBlobToken;
        }

        private static IConfigurationRoot BuildAppConfig(ExecutionContext context)
        {
            var config = new ConfigurationBuilder().SetBasePath
            (context.FunctionAppDirectory).AddJsonFile(LocalSettingFile,
            optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();
            return config;
        }
    }
}
