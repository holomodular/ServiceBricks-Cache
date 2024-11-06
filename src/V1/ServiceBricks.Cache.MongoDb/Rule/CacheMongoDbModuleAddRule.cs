﻿using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class CacheMongoDbModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<CacheMongoDbModule>),
                typeof(CacheMongoDbModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<CacheMongoDbModule>),
                typeof(CacheMongoDbModuleAddRule));
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
            var e = context.Object as ModuleAddEvent<CacheMongoDbModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;
            //var configuration = e.Configuration;

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            // AI: Add the storage services for the module for each domain object
            services.AddScoped<IStorageRepository<CacheData>, CacheStorageRepository<CacheData>>();

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<CacheDataDto>, CacheDataApiService>();
            services.AddScoped<ICacheDataApiService, CacheDataApiService>();

            // AI: Add business rules for the module
            DomainCreateUpdateDateRule<CacheData>.Register(BusinessRuleRegistry.Instance);
            DomainDateTimeOffsetRule<CacheData>.Register(BusinessRuleRegistry.Instance, nameof(CacheData.ExpirationDate));
            ApiConcurrencyByUpdateDateRule<CacheData, CacheDataDto>.Register(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<CacheData>.Register(BusinessRuleRegistry.Instance, "StorageKey", "Id");

            return response;
        }
    }
}