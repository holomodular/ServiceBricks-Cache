using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// This is persisted key/value data.
    /// </summary>
    public partial class CacheData : AzureDataTablesDomainObject<CacheData>, IDpCreateDate, IDpUpdateDate
    {
        /// <summary>
        /// The cache key.
        /// </summary>
        public string CacheKey { get; set; }

        /// <summary>
        /// The value.
        /// </summary>
        public string CacheValue { get; set; }

        /// <summary>
        /// The creation date.
        /// </summary>
        public DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// The update date.
        /// </summary>
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>
        /// The expiration date.
        /// </summary>
        public DateTimeOffset? ExpirationDate { get; set; }
    }
}