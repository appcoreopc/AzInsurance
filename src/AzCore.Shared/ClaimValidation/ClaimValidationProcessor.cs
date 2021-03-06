﻿using AzCore.Shared.TableStorage;
using AzCore.Shared.TableStorage.Entity;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace AzCore.Shared.ClaimValidation
{
    public class ClaimValidationProcessor
    {
        private readonly string connectionString;
        private readonly string tableName;

        public ClaimValidationProcessor(string connectionString, string tableName)
        {
            this.connectionString = connectionString;
            this.tableName = tableName;
        }

        public async Task<ClaimTableEntity> ProcessAsync(ClaimForm claim)
        {
            var table = await TableStoreFactory.CreateAsync(this.connectionString, this.tableName);
            var provider = new TableStorageProvider(table);

            TableOperation retrieveOperation = TableOperation.Retrieve<ClaimTableEntity>(claim.Id.ToString(), claim.Id.ToString());
            var result = await provider.QuerySingleAsync(retrieveOperation);
            var target = result.Result as ClaimTableEntity;
            return target;
        }
    }
}