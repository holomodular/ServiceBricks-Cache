using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.SqlServer
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache SqlServer module.
    /// </summary>
    public partial class CacheSqlServerModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance.
        /// </summary>
        public static CacheSqlServerModule Instance = new CacheSqlServerModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheSqlServerModule()
        {
            DependentModules = new List<IModule>()
            {
                new CacheEntityFrameworkCoreModule()
            };
        }
    }
}