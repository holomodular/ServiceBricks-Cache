using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// Extensions for adding the ServiceBricks Cache module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        
        /// <summary>
        /// Add the ServiceBricks Cache client to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheClient(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add clients for the module for each DTO
            services.AddScoped<IApiClient<CacheDataDto>, CacheDataApiClient>();
            services.AddScoped<ICacheDataApiClient, CacheDataApiClient>();

            return services;
        }

        /// <summary>
        /// Add the ServiceBricks Cache client to the service collection for the API Service references
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheClientForService(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Configure options for service usage            
            services.Configure<SemaphoreOptions>(configuration.GetSection(CacheModelConstants.APPSETTING_SEMAPHORE_OPTIONS));            

            // AI: Add clients for the API Services
            services.AddScoped<IApiService<CacheDataDto>, CacheDataApiClient>();
            services.AddScoped<ICacheDataApiService, CacheDataApiClient>();

            // AI: Add services
            services.AddScoped<ISingleServerProcessService, SingleServerProcessService>();
            services.AddScoped<ISemaphoreService, SemaphoreService>();

            return services;
        }
    }
}