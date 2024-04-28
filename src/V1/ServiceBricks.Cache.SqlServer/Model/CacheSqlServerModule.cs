using ServiceBricks.Cache.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Cache.SqlServer
{
    public class CacheSqlServerModule : IModule
    {
        public CacheSqlServerModule()
        {
            DependentModules = new List<IModule>()
            {
                new CacheEntityFrameworkCoreModule()
            };
        }

        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }
        public List<IModule> DependentModules { get; }
    }
}