using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.Cosmos
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache Cosmos module.
    /// </summary>
    public partial class CacheCosmosModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static CacheCosmosModule Instance = new CacheCosmosModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheCosmosModule()
        {
            DependentModules = new List<IModule>()
            {
                new CacheEntityFrameworkCoreModule()
            };
        }
    }
}