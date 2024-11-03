using Microsoft.EntityFrameworkCore;
using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.Cosmos
{
    /// <summary>
    /// This is the database context for the Cache module.
    /// </summary>
    public partial class CacheCosmosContext : DbContext
    {
        protected DbContextOptions<CacheCosmosContext> _options = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public CacheCosmosContext(DbContextOptions<CacheCosmosContext> options) : base(options)
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

            // AI: Create the model for each table
            builder.Entity<CacheData>().HasKey(key => key.CacheKey);
            builder.Entity<CacheData>().HasPartitionKey(key => key.CacheKey);
            builder.Entity<CacheData>().HasIndex(key => new { key.ExpirationDate }); // For background process
            builder.Entity<CacheData>().ToContainer(CacheCosmosConstants.GetContainerName(nameof(CacheData)));
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual CacheCosmosContext CreateDbContext(string[] args)
        {
            return new CacheCosmosContext(_options);
        }
    }
}