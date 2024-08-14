using Azure.Data.Tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// Extensions for starting the ServiceBricks Cache Azure Data Tables module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to indicate if the module has been started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Cache Azure Data Tables module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksCacheAzureDataTables(this IApplicationBuilder applicationBuilder)
        {
            // AI: Get the connection string
            var configuration = applicationBuilder.ApplicationServices.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetAzureDataTablesConnectionString(
                CacheAzureDataTablesConstants.APPSETTING_CONNECTION_STRING);

            // AI: Create each table in the module
            TableClient tableClient = new TableClient(
                connectionString,
                CacheAzureDataTablesConstants.GetTableName(nameof(CacheData)));
            tableClient.CreateIfNotExists();

            // AI: Set the module started flag
            ModuleStarted = true;

            // AI: Start parent module
            applicationBuilder.StartServiceBricksCache();

            return applicationBuilder;
        }
    }
}