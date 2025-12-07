using System.Reflection;

namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// The module definition for the ServiceBricks Cache MongoDb module.
    /// </summary>
    public partial class CacheMongoDbModule : ServiceBricks.Module
    {
        public static CacheMongoDbModule Instance = new CacheMongoDbModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheMongoDbModule()
        {
            DependentModules = new List<IModule>()
            {
                new CacheModule()
            };
        }
    }
}