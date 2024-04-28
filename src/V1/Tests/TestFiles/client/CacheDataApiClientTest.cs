using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceBricks.Xunit;

namespace ServiceBricks.Cache.Client.Xunit
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class CacheDataApiClientTest : ApiClientTest<CacheDataDto>
    {
        public CacheDataApiClientTest()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(ClientStartup));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<CacheDataDto>>();
        }
    }
}