using AutoMapper;

using ServiceQuery;

namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// This is a API service for the Data domain object.
    /// </summary>
    public class CacheDataApiService : ApiService<CacheData, CacheDataDto>, ICacheDataApiService
    {
        public CacheDataApiService(
            IMapper mapper,
            IBusinessRuleService businessRuleService,
            IDomainRepository<CacheData> repository)
            : base(mapper, businessRuleService, repository)
        {
        }
    }
}