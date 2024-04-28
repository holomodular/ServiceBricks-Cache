using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Cache.SqlServer
{
    /// <summary>
    /// This is the storage repository for the Cache module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public class CacheStorageRepository<TDomain> : EntityFrameworkCoreStorageRepository<TDomain>
        where TDomain : class, IEntityFrameworkCoreDomainObject<TDomain>, new()
    {
        public CacheStorageRepository(ILoggerFactory logFactory, CacheSqlServerContext context)
            : base(logFactory)
        {
            Context = context;
            DbSet = context.Set<TDomain>();
        }
    }
}