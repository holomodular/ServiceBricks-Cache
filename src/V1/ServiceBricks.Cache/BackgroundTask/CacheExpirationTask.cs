using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using System;
using System.Threading;
using System.Threading.Tasks;
using ServiceQuery;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This is a background task that queries for expired data and deletes them.
    /// </summary>
    public static partial class CacheExpirationTask
    {
        public static void QueueWork(this ITaskQueue backgroundTaskQueue)
        {
            backgroundTaskQueue.Queue(new Detail());
        }

        public class Detail : ITaskDetail<Detail, Worker>
        {
            public Detail()
            {
            }
        }

        public class Worker : ITaskWork<Detail, Worker>
        {
            private readonly ILogger<Worker> _logger;
            private readonly ICacheDataApiService _dataApiService;

            public Worker(
                ILogger<Worker> logger,
                ICacheDataApiService dataApiService)
            {
                _logger = logger;
                _dataApiService = dataApiService;
            }

            public async Task DoWork(Detail detail, CancellationToken cancellationToken)
            {
                var query = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(
                    nameof(CacheDataDto.ExpirationDate), DateTimeOffset.UtcNow.ToString("o"))
                    .Build();

                var respExpired = await _dataApiService.QueryAsync(query);
                if (respExpired.Error || respExpired.Item.List == null || respExpired.Item.List.Count == 0)
                    return;

                foreach (var item in respExpired.Item.List)
                {
                    var respDelete = await _dataApiService.DeleteAsync(item.StorageKey);
                }
            }
        }
    }
}