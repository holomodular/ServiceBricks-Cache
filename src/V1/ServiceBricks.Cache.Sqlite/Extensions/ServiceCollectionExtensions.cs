using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.EntityFrameworkCore;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.Sqlite
{
    /// <summary>
    /// IServiceCollection extensions for the Cache Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksCacheSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(CacheSqliteModule), new CacheSqliteModule());

            // Add Core service
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            //Register Database
            var builder = new DbContextOptionsBuilder<CacheSqliteContext>();
            string connectionString = configuration.GetSqliteConnectionString(
                CacheSqliteConstants.APPSETTING_DATABASE_CONNECTION);
            builder.UseSqlite(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
            });
            services.Configure<DbContextOptions<CacheSqliteContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<CacheSqliteContext>>(builder.Options);
            services.AddDbContext<CacheSqliteContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Storage Services
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            return services;
        }
    }
}