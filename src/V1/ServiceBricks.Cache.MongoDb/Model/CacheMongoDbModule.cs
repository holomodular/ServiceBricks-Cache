using System.Reflection;

namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache MongoDb module.
    /// </summary>
    public partial class CacheMongoDbModule : IModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheMongoDbModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(CacheMongoDbModule).Assembly
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