using AutoMapper;

namespace ServiceBricks.Cache.EntityFrameworkCore
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
            // AI: Add mappings for CacheDataDto and CacheData
            CreateMap<CacheDataDto, CacheData>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.CacheKey, y => y.MapFrom(z => string.IsNullOrEmpty(z.CacheKey) ? z.StorageKey : z.CacheKey));

            CreateMap<CacheData, CacheDataDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.CacheKey));
        }
    }
}