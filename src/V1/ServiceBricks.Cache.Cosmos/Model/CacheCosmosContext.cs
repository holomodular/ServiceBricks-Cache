using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ServiceBricks.Storage.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ServiceBricks.Cache.EntityFrameworkCore;

namespace ServiceBricks.Cache.Cosmos
{
    // dotnet ef migrations add CacheV1 --context CacheCosmosContext --startup-project ../Test/MigrationsHost

    /// <summary>
    /// This is the database context for the Cache module.
    /// </summary>
    public partial class CacheCosmosContext : DbContext
    {
        private DbContextOptions<CacheCosmosContext> _options = null;

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

            builder.Model.SetDefaultContainer(CacheCosmosConstants.DEFAULT_CONTAINER_NAME);

            builder.Entity<CacheData>().HasKey(key => key.Key);
        }
    }
}