using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.EntityFrameworkCore;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.InMemory
{
    /// <summary>
    /// IServiceCollection extensions for the Cache Brick.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksCacheInMemory(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(CacheInMemoryModule), new CacheInMemoryModule());

            // Add Core service
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            // Register Database
            var builder = new DbContextOptionsBuilder<CacheInMemoryContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString(), b => b.EnableNullChecks(false));
            services.Configure<DbContextOptions<CacheInMemoryContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<CacheInMemoryContext>>(builder.Options);
            services.AddDbContext<CacheInMemoryContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Storage Services
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            return services;
        }
    }
}