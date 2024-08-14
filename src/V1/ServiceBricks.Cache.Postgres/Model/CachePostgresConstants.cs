namespace ServiceBricks.Cache.Postgres
{
    /// <summary>
    /// These are constants for the ServiceBricks Cache Postgres module.
    /// </summary>
    public static partial class CachePostgresConstants
    {
        /// <summary>
        /// AppSetting keys for the Postgres connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Cache:Storage:Postgres:ConnectionString";

        /// <summary>
        /// The default schema name.
        /// </summary>
        public const string DATABASE_SCHEMA_NAME = "Cache";
    }
}