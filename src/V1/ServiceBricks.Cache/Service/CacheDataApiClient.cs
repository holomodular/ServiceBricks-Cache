using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ServiceBricks.Cache
{
    public class CacheDataApiClient : ApiClient<CacheDataDto>, ICacheDataApiClient
    {
        protected readonly IConfiguration _configuration;

        public CacheDataApiClient(
            ILoggerFactory loggerFactory,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
            : base(loggerFactory, httpClientFactory, configuration.GetApiConfig(CacheConstants.APPSETTING_CLIENT_APICONFIG))
        {
            ApiResource = @"Cache/CacheData";
        }
    }
}