using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// Extensions for adding the ServiceBricks Cache module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCache(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(CacheModule.Instance);

            // AI: Add module business rules
            CacheModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<CacheModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }

        /// <summary>
        /// Add the ServiceBricks Cache client to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheClient(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add clients for the module for each DTO
            services.AddScoped<IApiClient<CacheDataDto>, CacheDataApiClient>();
            services.AddScoped<ICacheDataApiClient, CacheDataApiClient>();

            return services;
        }
    }
}