using Microsoft.Azure.Amqp.Framing;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// This is an mapping profile for the CacheData domain object.
    /// </summary>
    public partial class CacheDataMappingProfile
    {
        /// <summary>
        /// Register the mapping
        /// </summary>
        public static void Register(IMapperRegistry registry)
        {
            registry.Register<CacheData, CacheDataDto>(
                (s, d) =>
                {
                    d.CacheKey = s.CacheKey;
                    d.CacheValue = s.CacheValue;
                    d.CreateDate = s.CreateDate;
                    d.ExpirationDate = s.ExpirationDate;
                    d.StorageKey = s.PartitionKey;
                    d.UpdateDate = s.UpdateDate;
                });

            registry.Register<CacheDataDto, CacheData>(
                (s, d) =>
                {
                    if (string.IsNullOrEmpty(s.CacheKey))
                    {
                        d.CacheKey = s.StorageKey;
                        d.PartitionKey = s.StorageKey;
                    }
                    else
                    {
                        d.CacheKey = s.CacheKey;
                        d.PartitionKey = s.CacheKey;
                    }
                    d.CacheValue = s.CacheValue;
                    //d.CreateDate ignored
                    //d.ETag ignored
                    d.ExpirationDate = s.ExpirationDate;

                    d.RowKey = string.Empty;
                    //d.Timestamp ignored
                    d.UpdateDate = s.UpdateDate;
                });
        }
    }
}