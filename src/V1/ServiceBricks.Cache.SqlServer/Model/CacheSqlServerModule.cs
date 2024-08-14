using ServiceBricks.Cache.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Cache.SqlServer
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache SqlServer module.
    /// </summary>
    public partial class CacheSqlServerModule : IModule
    {
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