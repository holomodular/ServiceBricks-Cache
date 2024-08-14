namespace ServiceBricks.Cache.Cosmos
{
    /// <summary>
    /// These are constants for the ServiceBricks Cache Cosmos module.
    /// </summary>
    public static partial class CacheCosmosConstants
    {
        /// <summary>
        /// AppSetting key for the Cosmos connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Cache:Storage:Cosmos:ConnectionString";

        /// <summary>
        /// AppSetting key for the Cosmos database name.
        /// </summary>
        public const string APPSETTING_DATABASE = "ServiceBricks:Cache:Storage:Cosmos:Database";

        /// <summary>
        /// The default container name.
        /// </summary>
        public const string DEFAULT_CONTAINER_NAME = "Cache";
    }
}