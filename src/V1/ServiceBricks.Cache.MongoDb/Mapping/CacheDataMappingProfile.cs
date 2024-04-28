using AutoMapper;

using ServiceBricks.Storage.MongoDb;

namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// This is an automapper profile for the Data domain object.
    /// </summary>
    public class CacheDataMappingProfile : Profile
    {
        public CacheDataMappingProfile()
        {
            CreateMap<CacheDataDto, CacheData>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.Id, y => y.MapFrom(z => z.StorageKey));

            CreateMap<CacheData, CacheDataDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Id));
        }
    }
}