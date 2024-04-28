using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Cache.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Cache.Cosmos
{
    /// <summary>
    /// IServiceCollection extensions for Cache.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksCacheCosmos(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(CacheCosmosModule), new CacheCosmosModule());

            // Add core
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            // Register Database
            var builder = new DbContextOptionsBuilder<CacheCosmosContext>();
            string connectionString = configuration.GetCosmosConnectionString(
                CacheCosmosConstants.APPSETTING_CONNECTION);
            string database = configuration.GetCosmosDatabase(
                CacheCosmosConstants.APPSETTING_DATABASE);
            builder.UseCosmos(connectionString, database);
            services.Configure<DbContextOptions<CacheCosmosContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<CacheCosmosContext>>(builder.Options);
            services.AddDbContext<CacheCosmosContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Storage Services
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            return services;
        }
    }
}