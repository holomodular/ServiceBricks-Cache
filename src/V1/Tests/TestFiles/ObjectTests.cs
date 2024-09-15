using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Cache;
using ServiceBricks.Cache.EntityFrameworkCore;
using ServiceQuery;
using static ServiceBricks.Xunit.BusinessRuleTests;

namespace ServiceBricks.Xunit
{
    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class ObjectTests
    {
        public virtual ISystemManager SystemManager { get; set; }

        public ObjectTests()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
        }

        [Fact]
        public virtual async Task ExpirationTaskTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var taskQueue = SystemManager.ServiceProvider.GetRequiredService<ITaskQueue>();

            var apiservice = SystemManager.ServiceProvider.GetRequiredService<ICacheDataApiService>();

            var startdto = new CacheDataDto()
            {
                CacheKey = Guid.NewGuid().ToString(),
                ExpirationDate = DateTimeOffset.UtcNow.AddSeconds(-1)
            };

            var respcreate = apiservice.Create(startdto);
            Assert.True(respcreate.Success);
            Assert.True(respcreate.Item != null);

            var respg = apiservice.Get(respcreate.Item.StorageKey);
            Assert.True(respg.Success);
            Assert.True(respg.Item != null);

            CacheExpirationTask.QueueWork(taskQueue);
            CacheExpirationTask.Worker worker = new CacheExpirationTask.Worker(apiservice);
            await worker.DoWork(new CacheExpirationTask.Detail(), CancellationToken.None);

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(3000);
            while (apiservice.Get(respcreate.Item.StorageKey).Item != null)
            {
                if (cts.Token.IsCancellationRequested)
                    break;
            }

            respg = apiservice.Get(respcreate.Item.StorageKey);

            Assert.True(respg.Item == null);
        }

        [Fact]
        public virtual async Task ExpirationTimerTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var taskQueue = SystemManager.ServiceProvider.GetRequiredService<ITaskQueue>();

            var apiservice = SystemManager.ServiceProvider.GetRequiredService<ICacheDataApiService>();

            var timer = new CacheExpirationTimer(
                SystemManager.ServiceProvider,
                SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>());

            var startdto = new CacheDataDto()
            {
                CacheKey = Guid.NewGuid().ToString(),
                ExpirationDate = DateTimeOffset.UtcNow.AddSeconds(-1)
            };

            var respcreate = apiservice.Create(startdto);
            Assert.True(respcreate.Success);
            Assert.True(respcreate.Item != null);

            await timer.StartAsync(CancellationToken.None);

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(3000);
            while (apiservice.Get(respcreate.Item.StorageKey).Item != null)
            {
                if (cts.Token.IsCancellationRequested)
                    break;
            }

            await timer.StopAsync(CancellationToken.None);

            var respg = apiservice.Get(respcreate.Item.StorageKey);
            Assert.True(respg.Item == null);
        }

        [Fact]
        public virtual async Task SingleServiceProcessTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var taskQueue = SystemManager.ServiceProvider.GetRequiredService<ITaskQueue>();

            var apiservice = SystemManager.ServiceProvider.GetRequiredService<ICacheDataApiService>();

            SingleServerProcessService service = new SingleServerProcessService(
                SystemManager.ServiceProvider.GetRequiredService<ICacheDataApiService>());

            string keyName = nameof(SingleServerProcessService) + ":" + "test";

            var result = await service.ShouldThisServerRunForProcessAsync("test");
            Assert.True(result);

            // Get
            var respGet = apiservice.Get(keyName);
            Assert.True(respGet.Success);
            Assert.True(respGet.Item != null);

            // Update
            respGet.Item.CacheValue = Guid.NewGuid().ToString();
            var respUpdate = apiservice.Update(respGet.Item);

            result = await service.ShouldThisServerRunForProcessAsync("test");
            Assert.True(!result); // should not

            // take over process
            var storagerepo = SystemManager.ServiceProvider.GetRequiredService<IStorageRepository<CacheData>>();
            var respitem = storagerepo.Get(new CacheData() { CacheKey = keyName });
            respitem.Item.UpdateDate = DateTimeOffset.UtcNow.Subtract(service.HeartbeatTimeout);
            storagerepo.Update(respitem.Item);

            result = await service.ShouldThisServerRunForProcessAsync("test");
            Assert.True(result); // take over

            // Cleanup

            apiservice.Delete(keyName);
        }

        [Fact]
        public virtual Task ModuleTests()
        {
            CacheModule module = new CacheModule();
            var dep = module.DependentModules;
            var au = module.AutomapperAssemblies;
            var vi = module.ViewAssemblies;

            CacheEntityFrameworkCoreModule emod = new CacheEntityFrameworkCoreModule();
            var edep = emod.DependentModules;
            var eau = emod.AutomapperAssemblies;
            var evi = emod.ViewAssemblies;

            return Task.CompletedTask;
        }

        [Fact]
        public virtual Task AddCacheClientTests()
        {
            IServiceCollection services = new ServiceCollection();
            IConfiguration config = new ConfigurationBuilder().Build();

            services.AddServiceBricksCacheClient(config);
            return Task.CompletedTask;
        }
    }
}