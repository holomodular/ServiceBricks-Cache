using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.SqlServer
{
    /// <summary>
    /// IApplicationBuilder extensions for the Cache Brick.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksCacheSqlServer(this IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // Migrate
                var context = serviceScope.ServiceProvider.GetService<CacheSqlServerContext>();
                context.Database.EnsureCreated();
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