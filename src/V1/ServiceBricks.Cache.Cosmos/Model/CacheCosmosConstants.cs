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
        public const string CONTAINER_PREFIX = "Cache";

        /// <summary>
        /// Get the container name for the given table name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetContainerName(string tableName)
        {
            return CONTAINER_PREFIX + tableName;
        }
    }
}