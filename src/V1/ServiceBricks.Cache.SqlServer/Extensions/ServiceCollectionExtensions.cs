using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.SqlServer;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.SqlServer
{
    /// <summary>
    /// Extensions to add the ServiceBricks Cache SqlServer module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache SqlServer module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add parent module
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(CacheSqlServerModule.Instance);

            // AI: Add module business rules
            CacheSqlServerModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<CacheSqlServerModule>.Register(BusinessRuleRegistry.Instance);
            SqlServerDatabaseMigrationRule<CacheSqlServerModule, CacheSqlServerContext>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}