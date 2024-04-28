namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// This is constants for the Cache module.
    /// </summary>
    public static class CacheMongoDbConstants
    {
        public const string APPSETTINGS_CONNECTION_STRING = "ServiceBrick:Cache:MongoDb:ConnectionString";
        public const string APPSETTINGS_DATABASE_NAME = "ServiceBrick:Cache:MongoDb:DatabaseName";

        public const string COLLECTIONNAME_PREFIX = "Cache";

        public static string GetCollectionName(string tableName)
        {
            return COLLECTIONNAME_PREFIX + tableName;
        }
    }
}