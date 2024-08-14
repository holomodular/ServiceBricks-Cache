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
        public virtual DbSet<CacheData> CacheDatas { get; set; }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // AI: Set the default container name
            builder.Model.SetDefaultContainer(CacheCosmosConstants.DEFAULT_CONTAINER_NAME);

            // AI: Create the model for each table
            builder.Entity<CacheData>().HasKey(key => key.CacheKey);
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