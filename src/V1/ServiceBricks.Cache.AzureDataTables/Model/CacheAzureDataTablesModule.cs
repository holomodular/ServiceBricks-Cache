using System.Reflection;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache Azure Data Tables module.
    /// </summary>
    public partial class CacheAzureDataTablesModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static CacheAzureDataTablesModule Instance = new CacheAzureDataTablesModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheAzureDataTablesModule()
        {
            DependentModules = new List<IModule>()
            {
                new CacheModule()
            };
        }
    }
}