using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// This is the storage repository for the ServiceBricks.Cache.AzureDataTables domain object.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public partial class CacheStorageRepository<TDomain> : AzureDataTablesStorageRepository<TDomain>
        where TDomain : class, IAzureDataTablesDomainObject<TDomain>, new()
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logFactory"></param>
        /// <param name="configuration"></param>
        public CacheStorageRepository(
            ILoggerFactory logFactory,
            IConfiguration configuration)
            : base(logFactory)
        {
            ConnectionString = configuration.GetAzureDataTablesConnectionString(
                CacheAzureDataTablesConstants.APPSETTING_CONNECTION_STRING);
            TableName = CacheAzureDataTablesConstants.GetTableName(typeof(TDomain).Name);
        }
    }
}