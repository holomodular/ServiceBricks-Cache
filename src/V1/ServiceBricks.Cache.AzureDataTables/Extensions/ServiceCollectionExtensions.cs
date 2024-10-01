using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// Extensions for adding the ServiceBricks Cache Azure Data Tables module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache Azure Data Tables module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheAzureDataTables(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the parent module first
            services.AddServiceBricksCache(configuration);

            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(CacheAzureDataTablesModule.Instance);

            // AI: Add module business rules
            CacheAzureDataTablesModuleAddRule.Register(BusinessRuleRegistry.Instance);
            CacheAzureDataTablesModuleStartRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<CacheAzureDataTablesModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}