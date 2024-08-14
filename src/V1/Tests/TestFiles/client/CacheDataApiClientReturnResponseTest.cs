using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache;
using ServiceBricks.Cache.Client.Xunit;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class CacheDataApiClientReturnResponseTest : ApiClientReturnResponseTest<CacheDataDto>
    {
        public CacheDataApiClientReturnResponseTest()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(ClientStartup));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<CacheDataDto>>();
        }
    }
}