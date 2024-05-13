using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.EntityFrameworkCore;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.Postgres
{
    /// <summary>
    /// IServiceCollection extensions for the Cache Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksCachePostgres(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(CachePostgresModule), new CachePostgresModule());

            // Add Core service
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            //Register Database
            var builder = new DbContextOptionsBuilder<CachePostgresContext>();
            string connectionString = configuration.GetPostgresConnectionString(
                CachePostgresConstants.APPSETTING_CONNECTION_STRING);
            builder.UseNpgsql(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });
            services.Configure<DbContextOptions<CachePostgresContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<CachePostgresContext>>(builder.Options);
            services.AddDbContext<CachePostgresContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Storage Services
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            return services;
        }
    }
}