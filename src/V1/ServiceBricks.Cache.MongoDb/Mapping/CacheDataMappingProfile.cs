using AutoMapper;

namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// This is an automapper profile for the CacheData domain object.
    /// </summary>
    public partial class CacheDataMappingProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheDataMappingProfile()
        {
            // AI: Map the CacheDataDto to the CacheData
            CreateMap<CacheDataDto, CacheData>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Id, y => y.MapFrom(z => string.IsNullOrEmpty(z.CacheKey) ? z.StorageKey : z.CacheKey))
                .ForMember(x => x.CacheKey, y => y.MapFrom(z => string.IsNullOrEmpty(z.CacheKey) ? z.StorageKey : z.CacheKey));

            CreateMap<CacheData, CacheDataDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Id));
        }
    }
}