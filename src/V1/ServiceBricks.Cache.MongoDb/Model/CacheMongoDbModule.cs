using System.Reflection;

namespace ServiceBricks.Cache.MongoDb
{
    public class CacheMongoDbModule : IModule
    {
        public CacheMongoDbModule()
        {
            AdminHtml = string.Empty;
            Name = "Cache MongoDb Brick";
            Description = @"The Cache MongoDB Brick implements the MongoDB provider.";
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(CacheMongoDbModule).Assembly
            };
            DependentModules = new List<IModule>()
            {
                new CacheModule()
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