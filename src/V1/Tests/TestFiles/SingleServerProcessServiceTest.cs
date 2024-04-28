using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache;
using ServiceQuery;

namespace ServiceBricks.Xunit
{
    public class SingleServerProcessServiceTest : IDisposable
    {
        public SingleServerProcessServiceTest()
        {
            SystemManager = new SystemManager();
            SystemManager?.StartSystem(typeof(TestStartup));
        }

        public void Dispose()
        {
            SystemManager?.StopSystem();
        }

        public SystemManager SystemManager { get; set; }

        [Fact]
        public virtual async Task Update_CreateDate()
        {
            var ssps = SystemManager.ServiceProvider.GetRequiredService<ISingleServerProcessService>();
            var cacheDataApiService = SystemManager.ServiceProvider.GetRequiredService<ICacheDataApiService>();

            // Create new process
            string processName = Guid.NewGuid().ToString();
            var shouldRun = await ssps.ShouldThisServerRunForProcessAsync(processName);
            Assert.True(shouldRun);

            // Run again
            shouldRun = await ssps.ShouldThisServerRunForProcessAsync(processName);
            Assert.True(shouldRun);

            // Find item
            var request = ServiceQueryRequestBuilder.New().Contains(nameof(CacheDataDto.Key), processName).Build();
            var respQuery = await cacheDataApiService.QueryAsync(request);
            if (respQuery.Error)
                Assert.Fail("Error");
            if (respQuery.Item.List.Count != 1)
                Assert.Fail("Too many found");
            var existingItem = respQuery.Item.List[0];

            // Update to different server
            existingItem.Value = Guid.NewGuid().ToString();
            await cacheDataApiService.UpdateAsync(existingItem);

            // Run again (should not run)
            shouldRun = await ssps.ShouldThisServerRunForProcessAsync(processName);
            Assert.True(!shouldRun);
        }
    }
}