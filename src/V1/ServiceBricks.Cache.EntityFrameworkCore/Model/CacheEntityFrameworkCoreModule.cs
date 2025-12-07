using System.Reflection;

namespace ServiceBricks.Cache.EntityFrameworkCore
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache EntityFrameworkCore module.
    /// </summary>
    public partial class CacheEntityFrameworkCoreModule : ServiceBricks.Module
    {
        public static CacheEntityFrameworkCoreModule Instance = new CacheEntityFrameworkCoreModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheEntityFrameworkCoreModule()
        {
        }
    }
}