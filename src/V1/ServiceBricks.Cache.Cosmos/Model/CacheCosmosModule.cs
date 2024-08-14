using System.Reflection;

namespace ServiceBricks.Cache.Cosmos
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache Cosmos module.
    /// </summary>
    public partial class CacheCosmosModule : IModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheCosmosModule()
        {
            DependentModules = new List<IModule>()
            {
                new EntityFrameworkCore.CacheEntityFrameworkCoreModule()
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