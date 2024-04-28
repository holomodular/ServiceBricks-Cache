using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// IServiceCollection extensions for Cache.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksCacheAzureDataTables(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(CacheAzureDataTablesModule), new CacheAzureDataTablesModule());

            // Add core
            services.AddServiceBricksCache(configuration);

            // Storage Services
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            // API Services
            services.AddScoped<IApiService<CacheDataDto>, CacheDataApiService>();
            services.AddScoped<ICacheDataApiService, CacheDataApiService>();

            // Business Rules
            DomainCreateUpdateDateRule<CacheData>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<CacheData>.RegisterRule(BusinessRuleRegistry.Instance, nameof(CacheData.ExpirationDate));
            ApiConcurrencyByUpdateDateRule<CacheData, CacheDataDto>.RegisterRule(BusinessRuleRegistry.Instance);
            CacheDataCreateRule.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<CacheData>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "PartitionKey");

            return services;
        }
    }
}