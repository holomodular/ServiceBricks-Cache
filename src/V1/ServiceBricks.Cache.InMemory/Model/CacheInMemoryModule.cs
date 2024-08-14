using ServiceBricks.Cache.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Cache.InMemory
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache InMemory module.
    /// </summary>
    public partial class CacheInMemoryModule : IModule
    {
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