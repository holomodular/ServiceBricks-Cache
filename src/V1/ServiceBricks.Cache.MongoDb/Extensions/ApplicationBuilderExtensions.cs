using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// Extensions to start the ServiceBricks Cache MongoDb module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to indicate if the module has been started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Cache MongoDb module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksCacheMongoDb(this IApplicationBuilder applicationBuilder)
        {
            // AI: Flag the module as started
            ModuleStarted = true;

            // AI: Start the parent module
            applicationBuilder.StartServiceBricksCache();

            return applicationBuilder;
        }
    }
}