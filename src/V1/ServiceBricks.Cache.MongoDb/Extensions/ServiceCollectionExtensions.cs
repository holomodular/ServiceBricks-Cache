using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// Extensions to add the ServiceBricks Cache MongoDb module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache MongoDb module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the parent module
            services.AddServiceBricksCache(configuration);

            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(CacheMongoDbModule.Instance);

            // AI: Add module business rules
            CacheMongoDbModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<CacheMongoDbModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}