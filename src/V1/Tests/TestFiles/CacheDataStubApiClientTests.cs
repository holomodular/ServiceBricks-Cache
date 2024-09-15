using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Xunit;
using Newtonsoft.Json;
using ServiceQuery;
using Microsoft.AspNetCore.Mvc;
using static ServiceBricks.Xunit.ApiClientTests;
using ServiceBricks.Cache;
using Microsoft.Extensions.Configuration;

namespace ServiceBricks.Xunit
{
    public class CacheDataStubTestManager : CacheDataTestManager
    {
        public class CacheDataHttpClientFactory : IHttpClientFactory
        {
            private ApiClientTests.CustomGenericHttpClientHandler<CacheDataDto> _handler;

            public CacheDataHttpClientFactory(ApiClientTests.CustomGenericHttpClientHandler<CacheDataDto> handler)
            {
                _handler = handler;
            }

            public HttpClient CreateClient(string name)
            {
                return new HttpClient(_handler);
            }
        }

        public override IApiClient<CacheDataDto> GetClient(IServiceProvider serviceProvider)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var newconfig = new Dictionary<string, string>()
            {
                ["ServiceBricks:Cache:Client:Api:BaseServiceUrl"] = "https://localhost:7000/api/v1.0",
            };
            var config = configurationBuilder
                .AddInMemoryCollection(newconfig).Build();
            var apioptions = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = false });
            var apiservice = serviceProvider.GetRequiredService<ICacheDataApiService>();
            var controller = new CacheDataApiController(apiservice, apioptions);
            var options = new OptionsWrapper<ClientApiOptions>(new ClientApiOptions() { ReturnResponseObject = false, BaseServiceUrl = "https://localhost:7000/", TokenUrl = "https://localhost:7000/token" });
            var handler = new ApiClientTests.CustomGenericHttpClientHandler<CacheDataDto>(controller);
            var clientHandlerFactory = new CacheDataHttpClientFactory(handler);
            return new CacheDataApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                clientHandlerFactory,
                config);
        }

        public ApiClientTests.CustomGenericHttpClientHandler<CacheDataDto> Handler { get; set; }

        public override IApiClient<CacheDataDto> GetClientReturnResponse(IServiceProvider serviceProvider)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var newconfig = new Dictionary<string, string>()
            {
                ["ServiceBricks:Cache:Client:Api:BaseServiceUrl"] = "https://localhost:7000/api/v1.0",
                ["ServiceBricks:Cache:Client:Api:ReturnResponseObject"] = "true",
            };
            var config = configurationBuilder
                .AddInMemoryCollection(newconfig).Build();

            var apioptions = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = true });
            var apiservice = serviceProvider.GetRequiredService<ICacheDataApiService>();
            var controller = new CacheDataApiController(apiservice, apioptions);

            var handler = new ApiClientTests.CustomGenericHttpClientHandler<CacheDataDto>(controller);
            var clientHandlerFactory = new CacheDataHttpClientFactory(handler);
            return new CacheDataApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                clientHandlerFactory,
                config);
        }
    }

    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class StubCacheDataApiClientTest : ApiClientTest<CacheDataDto>
    {
        public StubCacheDataApiClientTest()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
            TestManager = new CacheDataStubTestManager();
        }
    }

    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class StubCacheDataApiClientReturnResponseTests : ApiClientReturnResponseTest<CacheDataDto>
    {
        public StubCacheDataApiClientReturnResponseTests()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
            TestManager = new CacheDataStubTestManager();
        }
    }
}