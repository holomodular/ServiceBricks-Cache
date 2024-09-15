using ServiceQuery;
using System.Net;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This will allow only one server to process a task at a time as the number of application instances are increased.
    /// </summary>
    public partial class SingleServerProcessService : ISingleServerProcessService
    {
        private readonly ICacheDataApiService _cacheDataApiService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="CacheDataApiService"></param>
        public SingleServerProcessService(ICacheDataApiService CacheDataApiService)
        {
            _cacheDataApiService = CacheDataApiService;
            HeartbeatTimeout = TimeSpan.FromMinutes(5);
        }

        /// <summary>
        /// Timeout for heartbeat.
        /// </summary>
        public virtual TimeSpan HeartbeatTimeout { get; set; }

        /// <summary>
        /// Determines if this server should run for the given process.
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public virtual async Task<bool> ShouldThisServerRunForProcessAsync(string processName)
        {
            // AI: Get the server name
            string hostname = Dns.GetHostName();

            // AI: Create a unique key for the process
            string keyName = nameof(SingleServerProcessService) + ":" + processName;

            // AI: Create a query to get the existing record
            var request = new ServiceQueryRequestBuilder()
                .IsEqual(nameof(CacheDataDto.CacheKey), keyName)
                .Build();

            // AI: Query the cache data
            var respQuery = await _cacheDataApiService.QueryAsync(request);
            if (respQuery.Error || respQuery.Item == null)
                return false;

            // AI: If no record found, create a new one and return success
            if (respQuery.Item.List == null || respQuery.Item.List.Count == 0)
            {
                var respCreate = await _cacheDataApiService.CreateAsync(new CacheDataDto()
                {
                    CacheKey = keyName,
                    CacheValue = hostname
                });
                return respCreate.Success;
            }

            // AI: Get the first record
            var existingItem = respQuery.Item.List.OrderBy(x => x.CreateDate).First();

            // AI: If the value is the same as the server name, update the record so heartbeat updatedate is changed and return success
            if (string.Compare(existingItem.CacheValue, hostname, true) == 0)
            {
                var respUpdate = await _cacheDataApiService.UpdateAsync(existingItem);
                return respUpdate.Success;
            }

            // AI: Check if the heartbeat is older than the timeout, if so, take over
            DateTimeOffset timeout = DateTimeOffset.UtcNow.Subtract(HeartbeatTimeout);
            if (existingItem.UpdateDate <= timeout)
            {
                // AI: Update the record with the new server name
                existingItem.CacheValue = hostname;
                var respUpdate = await _cacheDataApiService.UpdateAsync(existingItem);
                return respUpdate.Success;
            }

            // AI: If the heartbeat is newer than the timeout, return false, this server should not run
            return false;
        }
    }
}