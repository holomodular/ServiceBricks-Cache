using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.InMemory
{
    /// <summary>
    /// Extensions to add the ServiceBricks Cache InMemory module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache InMemory module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheInMemory(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the parent module
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(CacheInMemoryModule.Instance);

            // AI: Add module business rules
            CacheInMemoryModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<CacheInMemoryModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}