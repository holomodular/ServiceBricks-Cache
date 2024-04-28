using Microsoft.Extensions.Logging;

namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// This is a business rule for the LogMessage object to set the
    /// partitionkey and rowkey of the object before create.
    /// </summary>
    public partial class CacheDataCreateRule : BusinessRule
    {
        /// <summary>
        /// Internal.
        /// </summary>
        protected readonly ILogger _logger;

        private readonly ITimezoneService _timezoneService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public CacheDataCreateRule(ILoggerFactory loggerFactory, ITimezoneService timezoneService)
        {
            _logger = loggerFactory.CreateLogger<CacheDataCreateRule>();
            _timezoneService = timezoneService;
            Priority = PRIORITY_LOW;
        }

        /// <summary>
        /// Register a rule for a domain object.
        /// </summary>
        public static void RegisterRule(IBusinessRuleRegistry registry)
        {
            registry.RegisterItem(
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
                if (context.Object is DomainCreateBeforeEvent<CacheData> ei)
                {
                    var item = ei.DomainObject;
                    item.PartitionKey = item.Key;
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