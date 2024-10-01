using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class CacheDataApiControllerTestCosmos : CacheDataApiControllerTest
    {
        public CacheDataApiControllerTestCosmos()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupCosmos));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<CacheDataDto>>();
        }
    }
}