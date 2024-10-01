using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.Sqlite
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache Sqlite module.
    /// </summary>
    public partial class CacheSqliteModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance.
        /// </summary>
        public static CacheSqliteModule Instance = new CacheSqliteModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheSqliteModule()
        {
            DependentModules = new List<IModule>()
            {
                new CacheEntityFrameworkCoreModule()
            };
        }
    }
}