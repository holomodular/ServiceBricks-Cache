using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Cache.Postgres
{
    /// <summary>
    /// This is the storage repository for the ServiceBricks Cache Postgres module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public partial class CacheStorageRepository<TDomain> : EntityFrameworkCoreStorageRepository<TDomain>
        where TDomain : class, IEntityFrameworkCoreDomainObject<TDomain>, new()
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logFactory"></param>
        /// <param name="context"></param>
        public CacheStorageRepository(ILoggerFactory logFactory, CachePostgresContext context)
            : base(logFactory)
        {
            Context = context;
            DbSet = context.Set<TDomain>();
        }
    }
}