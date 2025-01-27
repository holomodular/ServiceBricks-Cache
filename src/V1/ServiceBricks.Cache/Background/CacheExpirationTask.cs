using ServiceQuery;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This is a background task that queries for expired data and deletes them.
    /// </summary>
    public static partial class CacheExpirationTask
    {
        /// <summary>
        /// Queue the work to the background task queue.
        /// </summary>
        /// <param name="backgroundTaskQueue"></param>
        public static void QueueWork(this ITaskQueue backgroundTaskQueue)
        {
            backgroundTaskQueue.Queue(new Detail());
        }

        /// <summary>
        /// The detail class provides any additional information needed to perform the work.
        /// In this case, no additional information is needed.
        /// </summary>
        public class Detail : ITaskDetail<Detail, Worker>
        {
        }

        /// <summary>
        /// The worker class performs the work detail. It should also be registered as a scoped service
        /// </summary>
        public class Worker : ITaskWork<Detail, Worker>
        {
            private readonly ICacheDataApiService _dataApiService;

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="logger"></param>
            /// <param name="dataApiService"></param>
            public Worker(
                ICacheDataApiService dataApiService)
            {
                _dataApiService = dataApiService;
            }

            /// <summary>
            /// Perform the work detail.
            /// </summary>
            /// <param name="detail"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task DoWork(Detail detail, CancellationToken cancellationToken)
            {
                // AI: Create a query to find expired cache data.
                var query = new ServiceQueryRequestBuilder()
                    .IsNotNull(nameof(CacheDataDto.ExpirationDate))
                    .And()
                    .IsLessThanOrEqual(nameof(CacheDataDto.ExpirationDate), DateTimeOffset.UtcNow.ToString("o"))
                    .Build();

                // AI: Query for expired cache data.
                var respExpired = await _dataApiService.QueryAsync(query);
                if (respExpired.Error || respExpired.Item == null || respExpired.Item.List == null || respExpired.Item.List.Count == 0)
                    return;

                foreach (var item in respExpired.Item.List)
                {
                    // AI: Delete the expired cache data.
                    await _dataApiService.DeleteAsync(item.StorageKey);
                }
            }
        }
    }
}