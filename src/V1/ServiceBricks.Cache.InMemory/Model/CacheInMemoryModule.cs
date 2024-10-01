using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.InMemory
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache InMemory module.
    /// </summary>
    public partial class CacheInMemoryModule : ServiceBricks.Module
    {
        public static CacheInMemoryModule Instance = new CacheInMemoryModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheInMemoryModule()
        {
            DependentModules = new List<IModule>()
            {
                new CacheEntityFrameworkCoreModule()
            };
        }
    }
}