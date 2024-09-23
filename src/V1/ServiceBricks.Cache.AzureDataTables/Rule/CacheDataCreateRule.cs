using Microsoft.Extensions.Logging;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// This is a business rule for creating a CacheData domain object. It will set the PartitionKey and RowKey.
    /// </summary>
    public sealed class CacheDataCreateRule : BusinessRule
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public CacheDataCreateRule(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CacheDataCreateRule>();
            Priority = PRIORITY_LOW;
        }

        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(DomainCreateBeforeEvent<CacheData>),
                typeof(CacheDataCreateRule));
        }

        /// <summary>
        /// Unregister the rule
        /// </summary>
        /// <param name="registry"></param>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(DomainCreateBeforeEvent<CacheData>),
                typeof(CacheDataCreateRule));
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IResponse ExecuteRule(IBusinessRuleContext context)
        {
            var response = new Response();

            try
            {
                // AI: Make sure the context object is the correct type
                if (context.Object is DomainCreateBeforeEvent<CacheData> e)
                {
                    // AI: Set the PartitionKey and RowKey
                    var item = e.DomainObject;
                    item.PartitionKey = item.CacheKey;
                    item.RowKey = string.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.ERROR_BUSINESS_RULE));
            }

            return response;
        }
    }
}