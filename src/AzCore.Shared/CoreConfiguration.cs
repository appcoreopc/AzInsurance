using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;

namespace AzCore.Shared
{
    public class CoreConfiguration
    {
        public static IConfigurationRoot BuildAppConfig(ExecutionContext context)
        {
            var config = new ConfigurationBuilder().SetBasePath
            (context.FunctionAppDirectory).AddJsonFile(ApplicationConstants.SettingFilename, optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();
            return config;
        }
    }
}
