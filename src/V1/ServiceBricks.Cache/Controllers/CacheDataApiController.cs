using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This is an exposed REST-based API controller for the Data domain object.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/Cache/CacheData")]
    [Produces("application/json")]
    public class CacheDataApiController : AdminPolicyApiController<CacheDataDto>, ICacheDataApiController
    {
        protected readonly ICacheDataApiService _dataApiService;

        public CacheDataApiController(
            ICacheDataApiService dataApiService,
            IOptions<ApiOptions> apiOptions)
            : base(dataApiService, apiOptions)
        {
            _dataApiService = dataApiService;
        }
    }
}