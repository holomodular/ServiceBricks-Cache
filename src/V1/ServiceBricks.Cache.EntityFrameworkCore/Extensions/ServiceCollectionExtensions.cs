using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache.EntityFrameworkCore
{
    /// <summary>
    /// Extensions to add the ServiceBricks Cache EntityFrameworkCore module to the IServiceCollection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache EntityFrameworkCore module to the IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(CacheEntityFrameworkCoreModule), new CacheEntityFrameworkCoreModule());

            // AI: Add the parent module
            services.AddServiceBricksCache(configuration);

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<CacheDataDto>, CacheDataApiService>();
            services.AddScoped<ICacheDataApiService, CacheDataApiService>();

            // AI: Register business rules for the module
            DomainCreateUpdateDateRule<CacheData>.Register(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<CacheData>.Register(BusinessRuleRegistry.Instance, nameof(CacheData.ExpirationDate));
            ApiConcurrencyByUpdateDateRule<CacheData, CacheDataDto>.Register(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<CacheData>.Register(BusinessRuleRegistry.Instance, "StorageKey", "CacheKey");

            return services;
        }
    }
}