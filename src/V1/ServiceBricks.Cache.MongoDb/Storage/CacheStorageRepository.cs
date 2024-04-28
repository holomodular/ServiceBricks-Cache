using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.MongoDb;

namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// This is the storage repository for the Log module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public class CacheStorageRepository<TDomain> : MongoDbStorageRepository<TDomain>
        where TDomain : class, IMongoDbDomainObject<TDomain>, new()
    {
        public CacheStorageRepository(
            ILoggerFactory logFactory,
            IConfiguration configuration)
            : base(logFactory)
        {
            ConnectionString = configuration.GetMongoDbConnectionString(
                CacheMongoDbConstants.APPSETTINGS_CONNECTION_STRING);
            DatabaseName = configuration.GetMongoDbDatabaseName(
                CacheMongoDbConstants.APPSETTINGS_DATABASE_NAME);
            CollectionName = CacheMongoDbConstants.GetCollectionName(typeof(TDomain).Name);
        }
    }
}