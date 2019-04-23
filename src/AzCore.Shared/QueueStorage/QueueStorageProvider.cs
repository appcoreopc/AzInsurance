using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AzCore.Shared.QueueStorage
{
    public class QueueStorageProvider
    {
        private readonly CloudQueue cloudQueue;
        public QueueStorageProvider(CloudQueue cloudQueue)
        {
            this.cloudQueue = cloudQueue;
        }

        public async Task SendMessageAsync(CloudQueueMessage message) => await this.cloudQueue.AddMessageAsync(message);

        public async Task<CloudQueueMessage> PeekMessageAsync() => await this.cloudQueue.PeekMessageAsync();

        public async Task<CloudQueueMessage> GetMessageAsync() => await this.cloudQueue.GetMessageAsync();

        public int? GetQueueLength() => this.cloudQueue.ApproximateMessageCount;

        public async Task DeleteMessageAsync(CloudQueueMessage message) => await this.cloudQueue.DeleteMessageAsync(message);

        public async Task<IEnumerable<CloudQueueMessage>> GetMessages(int count) => await this.cloudQueue.GetMessagesAsync(count);

        public async Task<IEnumerable<CloudQueueMessage>> GetMessages(int count, TimeSpan? timeout) => await this.cloudQueue.GetMessagesAsync(count, timeout, null, null);

    }
}
