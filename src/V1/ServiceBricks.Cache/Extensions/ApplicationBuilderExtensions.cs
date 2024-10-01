using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// Extension methods for starting the ServiceBricks Cache module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Start the ServiceBricks Cache module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksCache(this IApplicationBuilder applicationBuilder)
        {
            // AI: Execute Module Start Event
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // Create business rule service
                var businessRuleService = serviceScope.ServiceProvider.GetRequiredService<IBusinessRuleService>();

                // Create event and execute
                var moduleStartEvent = new ModuleStartEvent<CacheModule>(
                    CacheModule.Instance,
                    applicationBuilder);
                businessRuleService.ExecuteEvent(moduleStartEvent);
            }

            return applicationBuilder;
        }
    }
}