using System.Collections.Generic;
using System.Reflection;

namespace ServiceBricks.Cache
{
    public class CacheModule : IModule
    {
        public CacheModule()
        {
            AdminHtml = string.Empty;
            Name = "Cache Brick";
            Description = @"The Cache Brick is responsible for managing cache data stored in key/value format.";
            ViewAssemblies = new List<Assembly>();
        }

        public string Name { get; }
        public string Description { get; }
        public string AdminHtml { get; }
        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }

        public List<IModule> DependentModules { get; }
    }
}