using System;
using System.Net;
using System.Threading.Tasks;
using ServiceQuery;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This will allow only one server to process a task at a time
    /// as the number of application instances are increased.
    /// </summary>
    public class SingleServerProcessService : ISingleServerProcessService
    {
        private readonly ICacheDataApiService _cacheDataApiService;

        public SingleServerProcessService(ICacheDataApiService CacheDataApiService)
        {
            _cacheDataApiService = CacheDataApiService;
            HeartbeatTimeout = TimeSpan.FromMinutes(5);
        }

        public TimeSpan HeartbeatTimeout { get; set; }

        public async Task<bool> ShouldThisServerRunForProcessAsync(string processName)
        {
            string hostname = Dns.GetHostName();

            string keyName = nameof(SingleServerProcessService) + ":" + processName;
            var request = ServiceQueryRequestBuilder.New().IsEqual(nameof(CacheDataDto.Key), keyName).Build();

            var respQuery = await _cacheDataApiService.QueryAsync(request);
            if (respQuery.Error || respQuery.Item == null)
                return false;

            //No server defined yet
            if (respQuery.Item.List == null || respQuery.Item.List.Count == 0)
            {
                var respCreate = await _cacheDataApiService.CreateAsync(new CacheDataDto()
                {
                    Key = keyName,
                    Value = hostname
                });
                return respCreate.Success;
            }

            var existingItem = respQuery.Item.List.OrderBy(x => x.CreateDate).First();

            //This server is primary
            if (string.Compare(existingItem.Value, hostname, true) == 0)
            {
                //Update record so updatedate is written
                var respUpdate = await _cacheDataApiService.UpdateAsync(existingItem);
                return respUpdate.Success;
            }

            //Check if server offline
            DateTimeOffset timeout = DateTimeOffset.UtcNow.Subtract(HeartbeatTimeout);
            if (existingItem.UpdateDate <= timeout)
            {
                //Take over
                existingItem.Value = hostname;
                var respUpdate = await _cacheDataApiService.UpdateAsync(existingItem);
                return respUpdate.Success;
            }
            return false;
        }
    }
}