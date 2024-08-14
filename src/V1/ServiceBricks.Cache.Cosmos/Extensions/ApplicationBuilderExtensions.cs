using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.Cosmos
{
    /// <summary>
    /// Extensions to start the ServiceBricks Cache Cosmos module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to indicate if the module has been started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Cache Cosmos module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksCacheCosmos(this IApplicationBuilder applicationBuilder)
        {
            // AI: Ensure the database is created
            using (var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CacheCosmosContext>();
                context.Database.EnsureCreated();
            }

            // AI: Set the module started flag
            ModuleStarted = true;

            // AI: Start the parent module
            // AI: If the primary keys of the Cosmos models do not match the EFC module, we can't use it rules, so skip EFC and call start on the core module instead.
            applicationBuilder.StartServiceBricksCacheEntityFrameworkCore();

            return applicationBuilder;
        }
    }
}