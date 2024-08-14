using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.InMemory
{
    /// <summary>
    /// Extensions to add the ServiceBricks Cache InMemory module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache InMemory module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheInMemory(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(CacheInMemoryModule), new CacheInMemoryModule());

            // AI: Add the parent module
            services.AddServiceBricksCacheEntityFrameworkCore(configuration);

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<CacheInMemoryContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString(), b => b.EnableNullChecks(false));
            services.Configure<DbContextOptions<CacheInMemoryContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<CacheInMemoryContext>>(builder.Options);
            services.AddDbContext<CacheInMemoryContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Add the storage services for the module for each domain object
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            return services;
        }
    }
}