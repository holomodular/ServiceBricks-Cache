using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ServiceBricks.Storage.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.SqlServer
{
    // dotnet ef migrations add CacheV1 --context CacheSqlServerContext --startup-project ../Test/MigrationsHost

    /// <summary>
    /// This is the database context for the Cache module.
    /// </summary>
    public partial class CacheSqlServerContext : DbContext, IDesignTimeDbContextFactory<CacheSqlServerContext>
    {
        private DbContextOptions<CacheSqlServerContext> _options = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheSqlServerContext()
        {
            var configBuider = new ConfigurationBuilder();
            configBuider.AddAppSettingsConfig();
            var configuration = configBuider.Build();

            var builder = new DbContextOptionsBuilder<CacheSqlServerContext>();
            string connectionString = configuration.GetSqlServerConnectionString(
                CacheSqlServerConstants.APPSETTING_CONNECTION_STRING);
            builder.UseSqlServer(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(CacheSqlServerContext).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });
            _options = builder.Options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public CacheSqlServerContext(DbContextOptions<CacheSqlServerContext> options) : base(options)
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
            builder.HasDefaultSchema(CacheSqlServerConstants.DATABASE_SCHEMA_NAME);

            builder.Entity<CacheData>().HasKey(key => key.Key);
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual CacheSqlServerContext CreateDbContext(string[] args)
        {
            return new CacheSqlServerContext(_options);
        }
    }
}