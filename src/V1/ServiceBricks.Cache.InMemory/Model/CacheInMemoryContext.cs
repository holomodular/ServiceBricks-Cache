using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ServiceBricks.Storage.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.InMemory
{
    // dotnet ef migrations add CacheV1 --context CacheInMemoryContext --startup-project ../Tests/WebApp

    /// <summary>
    /// This is the database context for the Cache module.
    /// </summary>
    public partial class CacheInMemoryContext : DbContext, IDesignTimeDbContextFactory<CacheInMemoryContext>
    {
        private DbContextOptions<CacheInMemoryContext> _options = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheInMemoryContext()
        {
            var configBuider = new ConfigurationBuilder();
            configBuider.AddAppSettingsConfig();
            var configuration = configBuider.Build();

            var builder = new DbContextOptionsBuilder<CacheInMemoryContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _options = builder.Options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public CacheInMemoryContext(DbContextOptions<CacheInMemoryContext> options) : base(options)
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
            //builder.HasDefaultSchema(CacheInMemoryConstants.DATABASE_SCHEMA_NAME);

            builder.Entity<CacheData>().HasKey(key => key.Key);
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual CacheInMemoryContext CreateDbContext(string[] args)
        {
            return new CacheInMemoryContext(_options);
        }
    }
}