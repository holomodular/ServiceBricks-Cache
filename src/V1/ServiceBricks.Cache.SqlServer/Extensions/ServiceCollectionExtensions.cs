using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

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
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(CacheSqlServerModule), new CacheSqlServerModule());

            // AI: Add parent module
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<CacheSqlServerContext>();
            string connectionString = configuration.GetSqlServerConnectionString(
                CacheSqlServerConstants.APPSETTING_CONNECTION_STRING);
            builder.UseSqlServer(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });
            services.Configure<DbContextOptions<CacheSqlServerContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<CacheSqlServerContext>>(builder.Options);
            services.AddDbContext<CacheSqlServerContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Add storage services for the module. Each domain object should have its own storage repository.
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            return services;
        }
    }
}