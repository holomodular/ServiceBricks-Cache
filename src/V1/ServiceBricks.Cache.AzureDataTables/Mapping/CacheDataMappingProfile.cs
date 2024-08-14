using AutoMapper;

using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// This is an mapping profile for the CacheData domain object.
    /// </summary>
    public partial class CacheDataMappingProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheDataMappingProfile()
        {
            // AI: Create a mapping profile for CacheDataDto and CacheData.
            CreateMap<CacheDataDto, CacheData>()
                .ForMember(x => x.CreateDate, y => y.Ignore())
                .ForMember(x => x.CacheKey, y => y.MapFrom(z => string.IsNullOrEmpty(z.CacheKey) ? z.StorageKey : z.CacheKey))
                .ForMember(x => x.PartitionKey, y => y.MapFrom<PartitionKeyResolver>())
                .ForMember(x => x.RowKey, y => y.MapFrom<RowKeyResolver>())
                .ForMember(x => x.ETag, y => y.Ignore())
                .ForMember(x => x.Timestamp, y => y.Ignore());

            CreateMap<CacheData, CacheDataDto>()
                .ForMember(x => x.StorageKey, y => y.MapFrom(z => z.CacheKey));
        }
    }
}