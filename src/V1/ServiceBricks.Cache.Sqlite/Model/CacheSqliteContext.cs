using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ServiceBricks.Storage.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ServiceBricks.Cache.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ServiceBricks.Cache.Sqlite
{
    // dotnet ef migrations add CacheV1 --context CacheSqliteContext --startup-project ../Test/MigrationsHost

    /// <summary>
    /// This is the database context for the Cache module.
    /// </summary>
    public partial class CacheSqliteContext : DbContext, IDesignTimeDbContextFactory<CacheSqliteContext>
    {
        private DbContextOptions<CacheSqliteContext> _options = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheSqliteContext()
        {
            var configBuider = new ConfigurationBuilder();
            configBuider.AddAppSettingsConfig();
            var configuration = configBuider.Build();

            var builder = new DbContextOptionsBuilder<CacheSqliteContext>();
            string connectionString = configuration.GetSqliteConnectionString(
                CacheSqliteConstants.APPSETTING_DATABASE_CONNECTION);
            builder.UseSqlite(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(CacheSqliteContext).Assembly.GetName().Name);
            });
            _options = builder.Options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public CacheSqliteContext(DbContextOptions<CacheSqliteContext> options) : base(options)
        {
            _options = options;
        }

        /// <summary>
        /// Cache Data.
        /// </summary>
        public virtual DbSet<CacheData> CacheData { get; set; }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Set default schema
            builder.HasDefaultSchema(CacheSqliteConstants.DATABASE_SCHEMA_NAME);

            builder.Entity<CacheData>().HasKey(key => key.Key);
        }

        /// <summary>
        /// Configure conventions
        /// </summary>
        /// <param name="configurationBuilder"></param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTimeOffset>()
            .HaveConversion<DateTimeOffsetToBytesConverter>();
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual CacheSqliteContext CreateDbContext(string[] args)
        {
            return new CacheSqliteContext(_options);
        }
    }
}