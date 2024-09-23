using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// Extensions for adding the ServiceBricks Cache Azure Data Tables module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache Azure Data Tables module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheAzureDataTables(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(CacheAzureDataTablesModule), new CacheAzureDataTablesModule());

            // AI: Add parent module
            services.AddServiceBricksCache(configuration);

            // AI: Configure all options for the module

            // AI: Add storage services for the module. Each domain object should have its own storage repository
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<CacheDataDto>, CacheDataApiService>();
            services.AddScoped<ICacheDataApiService, CacheDataApiService>();

            // AI: Register business rules for the module
            DomainCreateUpdateDateRule<CacheData>.Register(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<CacheData>.Register(BusinessRuleRegistry.Instance, nameof(CacheData.ExpirationDate));
            ApiConcurrencyByUpdateDateRule<CacheData, CacheDataDto>.Register(BusinessRuleRegistry.Instance);
            CacheDataCreateRule.Register(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<CacheData>.Register(BusinessRuleRegistry.Instance, "StorageKey", "PartitionKey");

            return services;
        }
    }
}