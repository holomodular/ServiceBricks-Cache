using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.Postgres
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache Postgres module.
    /// </summary>
    public partial class CachePostgresModule : ServiceBricks.Module
    {
        public static CachePostgresModule Instance = new CachePostgresModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public CachePostgresModule()
        {
            DependentModules = new List<IModule>()
            {
                new CacheEntityFrameworkCoreModule()
            };
        }
    }
}