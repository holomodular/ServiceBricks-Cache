namespace ServiceBricks.Cache
{
    /// <summary>
    /// This is the Data DTO.
    /// </summary>
    public partial class CacheDataDto : DataTransferObject
    {
        public CacheDataDto()
        {
        }

        public string Key { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public string Value { get; set; }
    }
}