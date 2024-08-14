using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.InMemory
{
    /// <summary>
    /// This is the database context for the Cache module.
    /// </summary>
    public partial class CacheInMemoryContext : DbContext, IDesignTimeDbContextFactory<CacheInMemoryContext>
    {
        protected DbContextOptions<CacheInMemoryContext> _options = null;

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

            // AI: Setup the entities to the model
            builder.Entity<CacheData>().HasKey(key => key.CacheKey);
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