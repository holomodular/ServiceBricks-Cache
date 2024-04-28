using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Cache.EntityFrameworkCore
{
    /// <summary>
    /// IServiceCollection extensions for Cache.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksCacheEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(CacheEntityFrameworkCoreModule), new CacheEntityFrameworkCoreModule());

            // Add core
            services.AddServiceBricksCache(configuration);

            // Storage Services
            //services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            // API Services
            services.AddScoped<IApiService<CacheDataDto>, CacheDataApiService>();
            services.AddScoped<ICacheDataApiService, CacheDataApiService>();

            // Business Rules
            DomainCreateUpdateDateRule<CacheData>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<CacheData>.RegisterRule(BusinessRuleRegistry.Instance, nameof(CacheData.ExpirationDate));
            ApiConcurrencyByUpdateDateRule<CacheData, CacheDataDto>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<CacheData>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            return services;
        }
    }
}