namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// This is a business rule for creating a CacheData domain object. It will set the PartitionKey and RowKey.
    /// </summary>
    public sealed class CacheDataCreateRule : BusinessRule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheDataCreateRule()
        {
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

            // AI: Make sure the context object is the correct type
            if (context == null || context.Object == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }
            var e = context.Object as DomainCreateBeforeEvent<CacheData>;
            if (e == null || e.DomainObject == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Set the PartitionKey and RowKey
            var item = e.DomainObject;
            item.PartitionKey = item.CacheKey;
            item.RowKey = string.Empty;

            return response;
        }
    }
}