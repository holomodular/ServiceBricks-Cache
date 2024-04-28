using System.Reflection;

namespace ServiceBricks.Cache.AzureDataTables
{
    public class CacheAzureDataTablesModule : IModule
    {
        public CacheAzureDataTablesModule()
        {
            AdminHtml = string.Empty;
            Name = "Cache AzureDataTables Brick";
            Description = @"The Cache AzureDataTables Brick implements the Azure Data Tables provider.";
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(CacheAzureDataTablesModule).Assembly
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