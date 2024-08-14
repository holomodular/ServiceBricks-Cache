namespace ServiceBricks.Cache.SqlServer
{
    /// <summary>
    /// These are constants for the ServiceBricks Cache SqlServer module.
    /// </summary>
    public static partial class CacheSqlServerConstants
    {
        /// <summary>
        /// AppSetting keys for the SqlServer connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Cache:Storage:SqlServer:ConnectionString";

        /// <summary>
        /// The default schema name for the database.
        /// </summary>
        public const string DATABASE_SCHEMA_NAME = "Cache";
    }
}