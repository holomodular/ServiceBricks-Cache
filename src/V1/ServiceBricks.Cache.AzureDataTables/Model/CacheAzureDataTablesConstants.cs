namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// This is constants for the Cache module.
    /// </summary>
    public static class CacheAzureDataTablesConstants
    {
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Cache:Storage:AzureDataTables:ConnectionString";

        public const string TABLENAME_PREFIX = "Cache";

        public static string GetTableName(string tableName)
        {
            return TABLENAME_PREFIX + tableName;
        }
    }
}