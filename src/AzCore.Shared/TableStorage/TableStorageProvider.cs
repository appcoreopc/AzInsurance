using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzCore.Shared.TableStorage
{
    public class TableStorageProvider
    {
        private readonly CloudTable cloudTable;

        public TableStorageProvider(CloudTable cloudTable)
        {
            this.cloudTable = cloudTable;
        }

        public async Task<(object, int)> Save(TableEntity entity)
        {
            TableOperation taskIntent = TableOperation.Insert(entity);
            var tableResult = await this.cloudTable.ExecuteAsync(taskIntent);
            return (tableResult.Result, tableResult.HttpStatusCode);
        }

        public async Task<IList<TableResult>> BatchSaveAsync(IEnumerable<TableEntity> entities)
        {
            TableBatchOperation batches = new TableBatchOperation();

            foreach (var item in entities)
            {
                batches.Insert(item);
            }

            return await this.cloudTable.ExecuteBatchAsync(batches);
        }
        public async Task<TableQuerySegment> QueryAsync(TableQuery queryOperation, TableContinuationToken token)
        {
            return await this.cloudTable.ExecuteQuerySegmentedAsync(queryOperation, token);
        }

        public async Task<TableResult> QuerySingleAsync(TableOperation queryOperation)
        {
            return await this.cloudTable.ExecuteAsync(queryOperation);
        }
    }
}
