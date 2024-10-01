using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache.EntityFrameworkCore
{
    /// <summary>
    /// Extensions to add the ServiceBricks Cache EntityFrameworkCore module to the IServiceCollection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache EntityFrameworkCore module to the IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the parent module
            services.AddServiceBricksCache(configuration);

            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(CacheEntityFrameworkCoreModule.Instance);

            // AI: Add module business rules
            CacheEntityFrameworkCoreModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<CacheEntityFrameworkCoreModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}