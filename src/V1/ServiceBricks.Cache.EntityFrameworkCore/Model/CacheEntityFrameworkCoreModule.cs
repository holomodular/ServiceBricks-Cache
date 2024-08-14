using System.Reflection;

namespace ServiceBricks.Cache.EntityFrameworkCore
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache EntityFrameworkCore module.
    /// </summary>
    public partial class CacheEntityFrameworkCoreModule : IModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheEntityFrameworkCoreModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(CacheEntityFrameworkCoreModule).Assembly
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