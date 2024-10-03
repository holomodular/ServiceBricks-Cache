using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Cache.Cosmos
{
    /// <summary>
    /// Extensions to add the ServiceBricks Cache Cosmos module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache Cosmos module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheCosmos(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the parent module
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(CacheCosmosModule.Instance);

            // AI: Add module business rules
            CacheCosmosModuleAddRule.Register(BusinessRuleRegistry.Instance);
            EntityFrameworkCoreDatabaseEnsureCreatedRule<CacheCosmosModule, CacheCosmosContext>.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<CacheCosmosModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}