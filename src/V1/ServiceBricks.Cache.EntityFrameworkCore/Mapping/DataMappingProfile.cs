using AutoMapper;

namespace ServiceBricks.Cache.EntityFrameworkCore
{
    /// <summary>
    /// This is an automapper profile for the Data domain object.
    /// </summary>
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<CacheDataDto, CacheData>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Key, y => y.MapFrom(z => string.IsNullOrEmpty(z.Key) ? z.StorageKey : z.Key));

            CreateMap<CacheData, CacheDataDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }
    }
}