using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// Extensions to add the ServiceBricks Cache MongoDb module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache MongoDb module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(CacheMongoDbModule), new CacheMongoDbModule());

            // AI: Add the parent module
            services.AddServiceBricksCache(configuration);

            // AI: Add the storage services for the module for each domain object
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<CacheDataDto>, CacheDataApiService>();
            services.AddScoped<ICacheDataApiService, CacheDataApiService>();

            // AI: Add business rules for the module
            DomainCreateUpdateDateRule<CacheData>.Register(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<CacheData>.Register(BusinessRuleRegistry.Instance, nameof(CacheData.ExpirationDate));
            ApiConcurrencyByUpdateDateRule<CacheData, CacheDataDto>.Register(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<CacheData>.Register(BusinessRuleRegistry.Instance, "StorageKey", "Id");

            return services;
        }
    }
}