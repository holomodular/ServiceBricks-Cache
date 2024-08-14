using AutoMapper;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// This is a REST API service for the CacheData domain object.
    /// </summary>
    public partial class CacheDataApiService : ApiService<CacheData, CacheDataDto>, ICacheDataApiService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="businessRuleService"></param>
        /// <param name="repository"></param>
        public CacheDataApiService(
            IMapper mapper,
            IBusinessRuleService businessRuleService,
            IDomainRepository<CacheData> repository)
            : base(mapper, businessRuleService, repository)
        {
        }
    }
}