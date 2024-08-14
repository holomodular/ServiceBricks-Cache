using System.Reflection;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache Azure Data Tables module.
    /// </summary>
    public partial class CacheAzureDataTablesModule : IModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheAzureDataTablesModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(CacheAzureDataTablesModule).Assembly
            };
            DependentModules = new List<IModule>()
            {
                new CacheModule()
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