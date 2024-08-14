namespace ServiceBricks.Cache.Sqlite
{
    /// <summary>
    /// These are constants for the ServiceBricks Cache Sqlite module.
    /// </summary>
    public static partial class CacheSqliteConstants
    {
        /// <summary>
        /// AppSetting keys for the Sqlite connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Cache:Storage:Sqlite:ConnectionString";

        /// <summary>
        /// The name of the database schema.
        /// </summary>
        public const string DATABASE_SCHEMA_NAME = "Cache";
    }
}