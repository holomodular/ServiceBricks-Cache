using Microsoft.AspNetCore.Builder;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.InMemory
{
    /// <summary>
    /// Extensions to start the ServiceBricks Cache InMemory module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to indicate if the module has been started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Cache InMemory module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksCacheInMemory(this IApplicationBuilder applicationBuilder)
        {
            // AI: Flag the module as started
            ModuleStarted = true;

            // AI: Start the parent module
            applicationBuilder.StartServiceBricksCacheEntityFrameworkCore();

            return applicationBuilder;
        }
    }
}