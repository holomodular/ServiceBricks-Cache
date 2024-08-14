using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// Extension methods for starting the ServiceBricks Cache module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to indicate if the module has started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Cache module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksCache(this IApplicationBuilder applicationBuilder)
        {
            // AI: Set the module started flag.
            ModuleStarted = true;

            return applicationBuilder;
        }
    }
}