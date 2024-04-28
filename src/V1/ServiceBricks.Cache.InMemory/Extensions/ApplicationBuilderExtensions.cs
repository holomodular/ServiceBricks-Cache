using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.InMemory
{
    /// <summary>
    /// IApplicationBuilder extensions for the Cache Brick.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksCacheInMemory(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;

            // Start Core Cache
            applicationBuilder.StartServiceBricksCacheEntityFrameworkCore();

            return applicationBuilder;
        }
    }
}