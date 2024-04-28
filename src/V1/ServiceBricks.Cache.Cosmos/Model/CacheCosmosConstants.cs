﻿namespace ServiceBricks.Cache.Cosmos
{
    /// <summary>
    /// Constants for the Cache module.
    /// </summary>
    public static class CacheCosmosConstants
    {
        public const string APPSETTING_CONNECTION = "ServiceBricks:Cache:Cosmos:ConnectionString";
        public const string APPSETTING_DATABASE = "ServiceBricks:Cache:Cosmos:Database";

        public const string DEFAULT_CONTAINER_NAME = "Cache";
    }
}