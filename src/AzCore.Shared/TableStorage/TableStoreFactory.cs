using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace AzCore.Shared.TableStorage
{
    public class TableStoreFactory
    {
        public static async Task<CloudTable> CreateAsync(string connectionString, string tableName)
        {
            var storage = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storage.CreateCloudTableClient();
            var cloudTable = tableClient.GetTableReference(tableName);
            await cloudTable.CreateIfNotExistsAsync();
            return cloudTable;
        }
    }
}
