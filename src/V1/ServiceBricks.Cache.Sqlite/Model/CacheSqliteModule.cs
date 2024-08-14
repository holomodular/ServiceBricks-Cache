using ServiceBricks.Cache.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Cache.Sqlite
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache Sqlite module.
    /// </summary>
    public partial class CacheSqliteModule : IModule
    {
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

        /// <summary>
        /// The list of dependent modules.
        /// </summary>
        public List<IModule> DependentModules { get; }

        /// <summary>
        /// The list of assemblies that contain automapper profiles.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// The list of assemblies that contain views.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}