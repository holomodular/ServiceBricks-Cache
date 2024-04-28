using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.Sqlite
{
    /// <summary>
    /// IApplicationBuilder extensions for the Cache Brick.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksCacheSqlite(this IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // Migrate
                var context = serviceScope.ServiceProvider.GetService<CacheSqliteContext>();
                context.Database.Migrate();
                context.SaveChanges();
            }
            ModuleStarted = true;

            // Start Core Cache
            applicationBuilder.StartServiceBricksCacheEntityFrameworkCore();

            return applicationBuilder;
        }
    }
}