using System.Reflection;

namespace ServiceBricks.Cache.Cosmos
{
    public class CacheCosmosModule : IModule
    {
        public CacheCosmosModule()
        {
            DependentModules = new List<IModule>()
            {
                new EntityFrameworkCore.CacheEntityFrameworkCoreModule()
            };
        }

        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }
        public List<IModule> DependentModules { get; }
    }
}