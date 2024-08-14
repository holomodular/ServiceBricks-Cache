using Microsoft.EntityFrameworkCore;
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
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(CacheCosmosModule), new CacheCosmosModule());

            // AI: Add the parent module
            // AI: If the primary keys of the Cosmos models do not match the EFC module, we can't use EFC rules, so skip EFC and call start on the core module instead.
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<CacheCosmosContext>();
            string connectionString = configuration.GetCosmosConnectionString(
                CacheCosmosConstants.APPSETTING_CONNECTION_STRING);
            string database = configuration.GetCosmosDatabase(
                CacheCosmosConstants.APPSETTING_DATABASE);
            builder.UseCosmos(connectionString, database);
            services.Configure<DbContextOptions<CacheCosmosContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<CacheCosmosContext>>(builder.Options);
            services.AddDbContext<CacheCosmosContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Storage Services for the module for each domain object
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            // AI: Register business rules for the module
            // AI: If the primary keys of the Cosmos models match the EFC module, we can use the EFC rules

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            // AI: If the primary keys of the Cosmos models match the EFC module, we can use the EFC rules

            return services;
        }
    }
}