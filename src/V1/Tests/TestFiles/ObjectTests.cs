using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Cache;
using ServiceBricks.Cache.EntityFrameworkCore;

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
        public virtual async Task ExpirationTaskQueueWorkTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var taskQueue = SystemManager.ServiceProvider.GetRequiredService<ITaskQueue>();

            var apiservice = SystemManager.ServiceProvider.GetRequiredService<ICacheDataApiService>();

            var startdto = new CacheDataDto()
            {
                CacheKey = Guid.NewGuid().ToString(),
                ExpirationDate = DateTimeOffset.UtcNow.AddSeconds(-1)
            };

            var respcreate = await apiservice.CreateAsync(startdto);
            Assert.True(respcreate.Success);
            Assert.True(respcreate.Item != null);

            var respg = await apiservice.GetAsync(respcreate.Item.StorageKey);
            Assert.True(respg.Success);
            Assert.True(respg.Item != null);

            CacheExpirationTask.QueueWork(taskQueue);

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(5000);
            while (!cts.Token.IsCancellationRequested)
            {
                var respTest = await apiservice.GetAsync(respcreate.Item.StorageKey);
                if (respTest.Item == null)
                    break;
            }

            respg = await apiservice.GetAsync(respcreate.Item.StorageKey);

            Assert.True(respg.Item == null);
        }

        [Fact]
        public virtual async Task ExpirationTaskMethodTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var taskQueue = SystemManager.ServiceProvider.GetRequiredService<ITaskQueue>();

            var apiservice = SystemManager.ServiceProvider.GetRequiredService<ICacheDataApiService>();

            var startdto = new CacheDataDto()
            {
                CacheKey = Guid.NewGuid().ToString(),
                ExpirationDate = DateTimeOffset.UtcNow.AddSeconds(-1)
            };

            var respcreate = await apiservice.CreateAsync(startdto);
            Assert.True(respcreate.Success);
            Assert.True(respcreate.Item != null);

            var respg = await apiservice.GetAsync(respcreate.Item.StorageKey);
            Assert.True(respg.Success);
            Assert.True(respg.Item != null);

            CacheExpirationTask.Worker worker = new CacheExpirationTask.Worker(apiservice);
            await worker.DoWork(new CacheExpirationTask.Detail(), CancellationToken.None);

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(5000);
            while (!cts.Token.IsCancellationRequested)
            {
                var respTest = await apiservice.GetAsync(respcreate.Item.StorageKey);
                if (respTest.Item == null)
                    break;
            }

            respg = await apiservice.GetAsync(respcreate.Item.StorageKey);

            Assert.True(respg.Item == null);
        }

        [Fact]
        public virtual async Task ExpirationTimerTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var taskQueue = SystemManager.ServiceProvider.GetRequiredService<ITaskQueue>();
            var apiservice = SystemManager.ServiceProvider.GetRequiredService<ICacheDataApiService>();
            var semaphoreOptions = new OptionsWrapper<ExpirationOptions>(new ExpirationOptions());

            var timer = new CacheExpirationTimer(
                SystemManager.ServiceProvider,
                SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>(),
                semaphoreOptions);

            var startdto = new CacheDataDto()
            {
                CacheKey = Guid.NewGuid().ToString(),
                ExpirationDate = DateTimeOffset.UtcNow.AddSeconds(-1)
            };

            var respcreate = await apiservice.CreateAsync(startdto);
            Assert.True(respcreate.Success);
            Assert.True(respcreate.Item != null);

            await timer.StartAsync(CancellationToken.None);

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(5000);
            while (!cts.Token.IsCancellationRequested)
            {
                var respTest = await apiservice.GetAsync(respcreate.Item.StorageKey);
                if (respTest.Item == null)
                    break;
            }

            await timer.StopAsync(CancellationToken.None);

            var respg = await apiservice.GetAsync(respcreate.Item.StorageKey);
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
            var respGet = await apiservice.GetAsync(keyName);
            Assert.True(respGet.Success);
            Assert.True(respGet.Item != null);

            // Update
            respGet.Item.CacheValue = Guid.NewGuid().ToString();
            var respUpdate = await apiservice.UpdateAsync(respGet.Item);

            result = await service.ShouldThisServerRunForProcessAsync("test");
            Assert.True(!result); // should not

            // take over process (storage repository skips all business rules)
            var storagerepo = SystemManager.ServiceProvider.GetRequiredService<IStorageRepository<CacheData>>();
            var respitem = storagerepo.Get(new CacheData() { CacheKey = keyName });
            respitem.Item.UpdateDate = DateTimeOffset.UtcNow.Subtract(service.HeartbeatTimeout);
            await storagerepo.UpdateAsync(respitem.Item);

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
            var vi = module.ViewAssemblies;

            CacheEntityFrameworkCoreModule emod = new CacheEntityFrameworkCoreModule();
            var edep = emod.DependentModules;
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

        [Fact]
        public virtual Task AddCacheClientForServiceTests()
        {
            IServiceCollection services = new ServiceCollection();
            IConfiguration config = new ConfigurationBuilder().Build();

            services.AddServiceBricksCacheClientForService(config);
            return Task.CompletedTask;
        }
    }
}