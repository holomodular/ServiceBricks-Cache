using Azure.Data.Tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// IApplicationBuilder extensions for Cache.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksCacheAzureDataTables(this IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var configuration = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();

                var connectionString = configuration.GetAzureDataTablesConnectionString(
                    CacheAzureDataTablesConstants.APPSETTING_CONNECTION_STRING);

                // Create each table if not exists
                TableClient tableClient = new TableClient(
                    connectionString,
                    CacheAzureDataTablesConstants.GetTableName(nameof(CacheData)));
                tableClient.CreateIfNotExists();
            }
            ModuleStarted = true;

            // Start core
            applicationBuilder.StartServiceBricksCache();

            return applicationBuilder;
        }
    }
}