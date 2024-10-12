using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.Postgres;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.Postgres
{
    /// <summary>
    /// Extensions to add the ServiceBricks Cache Postgres module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache Postgres module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCachePostgres(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add parent module
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(CachePostgresModule.Instance);

            // AI: Add module business rules
            CachePostgresModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<CachePostgresModule>.Register(BusinessRuleRegistry.Instance);
            PostgresDatabaseMigrationRule<CacheModule, CachePostgresContext>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}