using Azure;
using Azure.Data.Tables;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// This is persisted key/value data.
    /// </summary>
    public partial class CacheData : AzureDataTablesDomainObject<CacheData>, IDpCreateDate, IDpUpdateDate
    {
        public CacheData()
        {
        }

        public virtual string Key { get; set; }
        public virtual DateTimeOffset CreateDate { get; set; }
        public virtual DateTimeOffset UpdateDate { get; set; }
        public virtual DateTimeOffset? ExpirationDate { get; set; }
        public virtual string Value { get; set; }
    }
}