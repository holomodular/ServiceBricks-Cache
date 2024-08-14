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
        protected readonly ICacheDataApiService _dataApiService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataApiService"></param>
        /// <param name="apiOptions"></param>
        public CacheDataApiController(
            ICacheDataApiService dataApiService,
            IOptions<ApiOptions> apiOptions)
            : base(dataApiService, apiOptions)
        {
            _dataApiService = dataApiService;
        }
    }
}