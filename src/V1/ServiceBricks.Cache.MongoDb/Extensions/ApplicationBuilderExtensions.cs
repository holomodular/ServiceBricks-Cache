using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.MongoDb;

namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// IApplicationBuilder extensions for Cache.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksCacheMongoDb(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;

            // Start core
            applicationBuilder.StartServiceBricksCache();

            return applicationBuilder;
        }
    }
}