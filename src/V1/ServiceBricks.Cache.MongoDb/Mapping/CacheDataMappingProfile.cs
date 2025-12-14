namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// This is an automapper profile for the CacheData domain object.
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
                    d.StorageKey = s.CacheKey;
                    d.UpdateDate = s.UpdateDate;
                });

            registry.Register<CacheDataDto, CacheData>(
                (s, d) =>
                {
                    d.CacheKey = s.CacheKey;
                    d.CacheValue = s.CacheValue;
                    //d.CreateDate ignored
                    d.ExpirationDate = s.ExpirationDate;
                    d.UpdateDate = s.UpdateDate;
                    if (!string.IsNullOrEmpty(s.StorageKey))
                        d.CacheKey = s.StorageKey;
                });
        }
    }
}