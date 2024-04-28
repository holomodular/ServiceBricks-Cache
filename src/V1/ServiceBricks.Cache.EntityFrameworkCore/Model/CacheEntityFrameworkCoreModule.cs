using System.Reflection;

namespace ServiceBricks.Cache.EntityFrameworkCore
{
    public class CacheEntityFrameworkCoreModule : IModule
    {
        public CacheEntityFrameworkCoreModule()
        {
            AdminHtml = string.Empty;
            Name = "Cache EntityFrameworkCore Brick";
            Description = @"The Cache EntityFrameworkCore Brick implements the Entity Framework Core provider.";
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(CacheEntityFrameworkCoreModule).Assembly
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