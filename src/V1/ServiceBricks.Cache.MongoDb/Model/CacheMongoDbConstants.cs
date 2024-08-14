namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// These are constants for the ServiceBricks Cache MongoDb module.
    /// </summary>
    public static partial class CacheMongoDbConstants
    {
        /// <summary>
        /// AppSetting keys for the MongoDb connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Cache:Storage:MongoDb:ConnectionString";

        /// <summary>
        /// AppSetting keys for the database name.
        /// </summary>
        public const string APPSETTING_DATABASE = "ServiceBricks:Cache:Storage:MongoDb:Database";

        /// <summary>
        /// Prefix for the collection name.
        /// </summary>
        public const string COLLECTIONNAME_PREFIX = "Cache";

        /// <summary>
        /// Get the collection name for the table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetCollectionName(string tableName)
        {
            return COLLECTIONNAME_PREFIX + tableName;
        }
    }
}