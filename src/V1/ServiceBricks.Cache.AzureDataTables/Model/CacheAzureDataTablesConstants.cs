namespace ServiceBricks.Cache.AzureDataTables
{
    /// <summary>
    /// These are constants for the ServiceBricks Cache Azure Data Tables module.
    /// </summary>
    public static partial class CacheAzureDataTablesConstants
    {
        /// <summary>
        /// Application settings keys for the connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Cache:Storage:AzureDataTables:ConnectionString";

        /// <summary>
        /// The table name prefix.
        /// </summary>
        public const string TABLENAME_PREFIX = "Cache";

        /// <summary>
        /// Get the table name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetTableName(string tableName)
        {
            return TABLENAME_PREFIX + tableName;
        }
    }
}