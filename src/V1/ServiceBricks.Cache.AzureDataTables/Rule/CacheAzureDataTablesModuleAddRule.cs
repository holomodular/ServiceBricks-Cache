using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class CacheAzureDataTablesModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<CacheAzureDataTablesModule>),
                typeof(CacheAzureDataTablesModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<CacheAzureDataTablesModule>),
                typeof(CacheAzureDataTablesModuleAddRule));
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IResponse ExecuteRule(IBusinessRuleContext context)
        {
            var response = new Response();

            // AI: Make sure the context object is the correct type
            if (context == null || context.Object == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }
            var e = context.Object as ModuleAddEvent<CacheAzureDataTablesModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;
            //var configuration = e.Configuration;

            // AI: Configure all options for the module

            // AI: Add storage services for the module. Each domain object should have its own storage repository
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<CacheDataDto>, CacheDataApiService>();
            services.AddScoped<ICacheDataApiService, CacheDataApiService>();

            // AI: Register business rules for the module
            DomainCreateUpdateDateRule<CacheData>.Register(BusinessRuleRegistry.Instance);
            AzureDataTablesDomainDateTimeOffsetRule<CacheData>.Register(BusinessRuleRegistry.Instance, nameof(CacheData.ExpirationDate));
            ApiConcurrencyByUpdateDateRule<CacheData, CacheDataDto>.Register(BusinessRuleRegistry.Instance);
            CacheDataCreateRule.Register(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<CacheData>.Register(BusinessRuleRegistry.Instance, "StorageKey", "PartitionKey");
            DomainQueryPropertyRenameRule<CacheData>.Register(BusinessRuleRegistry.Instance, "CacheKey", "PartitionKey");

            return response;
        }
    }
}