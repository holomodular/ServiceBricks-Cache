using AutoMapper;

using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Cache.AzureDataTables
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
                .ForMember(x => x.Key, y => y.MapFrom(z => string.IsNullOrEmpty(z.Key) ? z.StorageKey : z.Key))
                .ForMember(x => x.PartitionKey, y => y.MapFrom<PartitionKeyResolver>())
                .ForMember(x => x.RowKey, y => y.MapFrom<RowKeyResolver>())
                .ForMember(x => x.ETag, y => y.Ignore())
                .ForMember(x => x.Timestamp, y => y.Ignore());

            CreateMap<CacheData, CacheDataDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.Key));
        }
    }
}