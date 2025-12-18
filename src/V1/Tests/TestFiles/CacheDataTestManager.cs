using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Cache;
using ServiceQuery;

namespace ServiceBricks.Xunit
{
    public class MongoDbCacheDataTestManager : CacheDataTestManager
    {
        public override CacheDataDto GetObjectNotFound()
        {
            return new CacheDataDto()
            {
                StorageKey = "000000000000000000000000"
            };
        }
    }

    public class CacheDataTestManagerPostgres : CacheDataTestManager
    {
        public override void ValidateObjects(CacheDataDto clientDto, CacheDataDto serviceDto, HttpMethod method)
        {
            //CreateDateRule
            if (method == HttpMethod.Post)
                Assert.True(serviceDto.CreateDate > clientDto.CreateDate);
            else if (method == HttpMethod.Get)
            {
                // Postgres special handling
                long utcTicks = serviceDto.CreateDate.UtcTicks;
                utcTicks = ((long)(utcTicks / 10)) * 10;
                Assert.True(utcTicks == clientDto.CreateDate.UtcTicks);
            }
            else
                Assert.True(serviceDto.CreateDate == clientDto.CreateDate);

            if (clientDto.ExpirationDate.HasValue && serviceDto.ExpirationDate.HasValue)
            {
                if (method == HttpMethod.Post)
                {
                    // Postgres special handling
                    long utcTicks = clientDto.ExpirationDate.Value.UtcTicks;
                    utcTicks = ((long)(utcTicks / 10)) * 10;
                    Assert.True(serviceDto.ExpirationDate.Value.UtcTicks >= utcTicks);
                }
                else if (method == HttpMethod.Put || method == HttpMethod.Patch)
                {
                    // Postgres special handling
                    long utcTicks = clientDto.ExpirationDate.Value.UtcTicks;
                    utcTicks = ((long)(utcTicks / 10)) * 10;
                    Assert.True(utcTicks == serviceDto.ExpirationDate.Value.UtcTicks);
                }
                else if (method == HttpMethod.Get)
                {
                    // Postgres special handling
                    long utcTicks = serviceDto.ExpirationDate.Value.UtcTicks;
                    utcTicks = ((long)(utcTicks / 10)) * 10;
                    Assert.True(utcTicks == clientDto.ExpirationDate.Value.UtcTicks);
                }
            }

            Assert.True(serviceDto.CacheKey == clientDto.CacheKey);

            Assert.True(serviceDto.CacheValue == clientDto.CacheValue);

            //UpdateDateRule
            if (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Patch)
                Assert.True(serviceDto.UpdateDate > clientDto.UpdateDate); //Rule
            else
            {
                // Postgres special handling
                long utcTicks = serviceDto.UpdateDate.UtcTicks;
                utcTicks = ((long)(utcTicks / 10)) * 10;
                Assert.True(utcTicks == clientDto.UpdateDate.UtcTicks);
            }
        }
    }

    public class CacheDataTestManager : TestManager<CacheDataDto>
    {
        public override CacheDataDto GetMinimumDataObject()
        {
            return new CacheDataDto()
            {
                CacheKey = Guid.NewGuid().ToString(),
            };
        }

        public override CacheDataDto GetMaximumDataObject()
        {
            var model = new CacheDataDto()
            {
                CreateDate = DateTimeOffset.UtcNow,
                ExpirationDate = DateTimeOffset.UtcNow.AddDays(1),
                CacheKey = Guid.NewGuid().ToString(),
                UpdateDate = DateTimeOffset.UtcNow,
                CacheValue = Guid.NewGuid().ToString(),
            };
            return model;
        }

        public override IApiController<CacheDataDto> GetController(IServiceProvider serviceProvider)
        {
            var options = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = false, ExposeSystemErrors = true });
            return new CacheDataApiController(serviceProvider.GetRequiredService<ICacheDataApiService>(), options);
        }

        public override IApiController<CacheDataDto> GetControllerReturnResponse(IServiceProvider serviceProvider)
        {
            var options = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = true, ExposeSystemErrors = true });
            return new CacheDataApiController(serviceProvider.GetRequiredService<ICacheDataApiService>(), options);
        }

        public override IApiClient<CacheDataDto> GetClient(IServiceProvider serviceProvider)
        {
            var appconfig = serviceProvider.GetRequiredService<IConfiguration>();
            var config = new ConfigurationBuilder()
                .AddConfiguration(appconfig)
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":ReturnResponseObject", "false" },
                })
                .Build();

            return new CacheDataApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                serviceProvider.GetRequiredService<IHttpClientFactory>(),
                config);
        }

        public override IApiClient<CacheDataDto> GetClientReturnResponse(IServiceProvider serviceProvider)
        {
            var appconfig = serviceProvider.GetRequiredService<IConfiguration>();
            var config = new ConfigurationBuilder()
                .AddConfiguration(appconfig)
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":ReturnResponseObject", "true" },
                })
                .Build();

            return new CacheDataApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                serviceProvider.GetRequiredService<IHttpClientFactory>(),
                config);
        }

        public override IApiService<CacheDataDto> GetService(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<ICacheDataApiService>();
        }

        public override void UpdateObject(CacheDataDto dto)
        {
            dto.ExpirationDate = DateTimeOffset.UtcNow.AddDays(1);
            dto.CacheValue = Guid.NewGuid().ToString();
        }

        public override void ValidateObjects(CacheDataDto clientDto, CacheDataDto serviceDto, HttpMethod method)
        {
            //CreateDateRule
            if (method == HttpMethod.Post)
                Assert.True(serviceDto.CreateDate > clientDto.CreateDate);
            else
                Assert.True(serviceDto.CreateDate == clientDto.CreateDate);

            Assert.True(serviceDto.ExpirationDate == clientDto.ExpirationDate);

            Assert.True(serviceDto.CacheKey == clientDto.CacheKey);

            Assert.True(serviceDto.CacheValue == clientDto.CacheValue);

            //UpdateDateRule
            if (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Patch)
                Assert.True(serviceDto.UpdateDate > clientDto.UpdateDate); //Rule
            else
                Assert.True(serviceDto.UpdateDate == clientDto.UpdateDate);
        }

        public override List<ServiceQueryRequest> GetQueriesForObject(CacheDataDto dto)
        {
            List<ServiceQueryRequest> queries = new List<ServiceQueryRequest>();

            var qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(CacheDataDto.CreateDate), dto.CreateDate.ToString("o"));
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(CacheDataDto.ExpirationDate), dto.ExpirationDate.Value.ToString("o"));
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(CacheDataDto.CacheKey), dto.CacheKey);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(CacheDataDto.StorageKey), dto.StorageKey);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(CacheDataDto.UpdateDate), dto.UpdateDate.ToString("o"));
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(CacheDataDto.CacheValue), dto.CacheValue);
            queries.Add(qb.Build());

            return queries;
        }
    }
}