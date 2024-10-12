namespace ServiceBricks.Cache
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache module.
    /// </summary>
    public partial class CacheModule : ServiceBricks.Module
    {
        public static CacheModule Instance = new CacheModule();

        public CacheModule()
        {
            DataTransferObjects = new List<Type>()
            {
                typeof(CacheDataDto),
            };
        }
    }
}