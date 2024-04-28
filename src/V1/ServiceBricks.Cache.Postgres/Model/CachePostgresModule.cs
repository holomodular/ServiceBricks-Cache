using ServiceBricks.Cache.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Cache.Postgres
{
    public class CachePostgresModule : IModule
    {
        public CachePostgresModule()
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