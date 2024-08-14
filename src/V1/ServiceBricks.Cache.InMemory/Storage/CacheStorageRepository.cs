using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Cache.InMemory
{
    /// <summary>
    /// This is the storage repository for the Cache module.
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
        public CacheStorageRepository(ILoggerFactory logFactory, CacheInMemoryContext context)
            : base(logFactory)
        {
            Context = context;
            DbSet = context.Set<TDomain>();
        }
    }
}