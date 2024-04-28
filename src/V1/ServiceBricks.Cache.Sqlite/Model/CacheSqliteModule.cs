using ServiceBricks.Cache.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Cache.Sqlite
{
    public class CacheSqliteModule : IModule
    {
        public CacheSqliteModule()
        {
            AdminHtml = string.Empty;
            Name = "Cache EntityFrameworkCore Brick";
            Description = @"The Cache EntityFrameworkCore Brick implements the EntityFrameworkCore provider.";
            DependentModules = new List<IModule>()
            {
                new CacheEntityFrameworkCoreModule()
            };
        }

        public string Name { get; }
        public string Description { get; }
        public string AdminHtml { get; }
        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }
        public List<IModule> DependentModules { get; }
    }
}