using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.Sqlite;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.Sqlite
{
    /// <summary>
    /// Extensions to add the ServiceBricks Cache Sqlite module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache Sqlite module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add parent module
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(CacheSqliteModule.Instance);

            // AI: Add module business rules
            CacheSqliteModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<CacheSqliteModule>.Register(BusinessRuleRegistry.Instance);
            SqliteDatabaseMigrationRule<CacheSqliteModule, CacheSqliteContext>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}