namespace ServiceBricks.Cache
{
    /// <summary>
    /// This is a CacheData data transfer object.
    /// </summary>
    public partial class CacheDataDto : DataTransferObject
    {
        /// <summary>
        /// The cache key.
        /// </summary>
        public virtual string CacheKey { get; set; }

        /// <summary>
        /// The value.
        /// </summary>
        public virtual string CacheValue { get; set; }

        /// <summary>
        /// The creation date.
        /// </summary>
        public virtual DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// The update date.
        /// </summary>
        public virtual DateTimeOffset UpdateDate { get; set; }

        /// <summary>
        /// The expiration date.
        /// </summary>
        public virtual DateTimeOffset? ExpirationDate { get; set; }
    }
}