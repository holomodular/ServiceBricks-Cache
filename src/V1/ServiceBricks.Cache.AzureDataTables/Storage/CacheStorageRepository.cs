using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// This is the storage repository for the Log module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public class CacheStorageRepository<TDomain> : AzureDataTablesStorageRepository<TDomain>
        where TDomain : class, IAzureDataTablesDomainObject<TDomain>, new()
    {
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