using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This is an exposed REST API controller for the CacheDataDto data transfer object.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/Cache/CacheData")]
    [Produces("application/json")]
    public partial class CacheDataApiController : AdminPolicyApiController<CacheDataDto>, ICacheDataApiController
    {
        protected readonly ICacheDataApiService _cacheDataApiService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataApiService"></param>
        /// <param name="apiOptions"></param>
        public CacheDataApiController(
            ICacheDataApiService cacheDataApiService,
            IOptions<ApiOptions> apiOptions)
            : base(cacheDataApiService, apiOptions)
        {
            _cacheDataApiService = cacheDataApiService;
        }
    }
}