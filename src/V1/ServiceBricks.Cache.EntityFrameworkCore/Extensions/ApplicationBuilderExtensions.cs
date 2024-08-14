using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Cache.EntityFrameworkCore
{
    /// <summary>
    /// Extensions to start the ServiceBricks Cache EntityFrameworkCore module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to indicate if the module has been started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Cache EntityFrameworkCore module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksCacheEntityFrameworkCore(this IApplicationBuilder applicationBuilder)
        {
            // AI: Set the module started flag when complete
            ModuleStarted = true;

            // AI: Start the core module
            applicationBuilder.StartServiceBricksCache();

            return applicationBuilder;
        }
    }
}