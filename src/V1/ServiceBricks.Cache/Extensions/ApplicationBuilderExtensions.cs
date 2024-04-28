using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// IApplicationBuilder extensions for Cache.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksCache(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;
            return applicationBuilder;
        }
    }
}