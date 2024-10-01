﻿using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class CacheModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<CacheModule>),
                typeof(CacheModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<CacheModule>),
                typeof(CacheModuleAddRule));
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
            var e = context.Object as ModuleAddEvent<CacheModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;

            // AI: Add hosted services for the module
            services.AddHostedService<CacheExpirationTimer>();

            // AI: Add workers for tasks in the module
            services.AddScoped<CacheExpirationTask.Worker>();

            // AI: Configure all options for the module

            // AI: Add API Controllers for each DTO in the module
            services.AddScoped<ICacheDataApiController, CacheDataApiController>();

            // AI: Add any miscellaneous services for the module
            services.AddScoped<ISingleServerProcessService, SingleServerProcessService>();

            // AI: Register business rules for the module

            // AI: Register servicebus subscriptions for the module

            return response;
        }
    }
}