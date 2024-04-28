using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache.EntityFrameworkCore
{
    /// <summary>
    /// IApplicationBuilder extensions for Cache.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksCacheEntityFrameworkCore(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;
            return applicationBuilder;
        }
    }
}