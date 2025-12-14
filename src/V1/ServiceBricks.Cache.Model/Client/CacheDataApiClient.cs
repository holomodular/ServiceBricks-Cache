using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This class is an REST API client for the CacheDataDto.
    /// </summary>
    public partial class CacheDataApiClient : ApiClient<CacheDataDto>, ICacheDataApiClient
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="httpClientFactory"></param>
        /// <param name="configuration"></param>
        public CacheDataApiClient(
            ILoggerFactory loggerFactory,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
            : base(loggerFactory, httpClientFactory, configuration.GetApiConfig(CacheModelConstants.APPSETTING_CLIENT_APICONFIG))
        {
            ApiResource = @"Cache/CacheData";
        }
    }
}