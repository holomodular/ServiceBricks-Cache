﻿namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// This is constants for the Cache module.
    /// </summary>
    public static class CacheMongoDbConstants
    {
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Cache:Storage:MongoDb:ConnectionString";
        public const string APPSETTING_DATABASE = "ServiceBricks:Cache:Storage:MongoDb:Database";

        public const string COLLECTIONNAME_PREFIX = "Cache";

        public static string GetCollectionName(string tableName)
        {
            return COLLECTIONNAME_PREFIX + tableName;
        }
    }
}