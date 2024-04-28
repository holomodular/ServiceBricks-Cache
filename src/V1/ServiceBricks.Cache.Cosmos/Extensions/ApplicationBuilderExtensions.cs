using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.Cosmos
{
    /// <summary>
    /// IApplicationBuilder extensions for Cache.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksCacheCosmos(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;

            using (var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CacheCosmosContext>();
                context.Database.EnsureCreated();
            }

            // Start Core Cache
            applicationBuilder.StartServiceBricksCacheEntityFrameworkCore();
            return applicationBuilder;
        }
    }
}