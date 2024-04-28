using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache;

namespace ServiceBricks.Xunit
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class CacheDataApiControllerTest : ApiControllerTest<CacheDataDto>
    {
        public CacheDataApiControllerTest()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<CacheDataDto>>();
        }

        [Fact]
        public virtual async Task Update_CreateDateAsync()
        {
            var model = TestManager.GetMinimumDataObject();
            var dto = await CreateBaseAsync(model);

            DateTimeOffset startingCreateDate = dto.CreateDate;

            //Update the CreateDate property
            dto.CreateDate = DateTime.UtcNow;

            //Call Update
            var controller = TestManager.GetController(SystemManager.ServiceProvider);
            var respUpdate = await controller.UpdateAsync(dto);
            if (respUpdate is OkObjectResult okResult)
            {
                Assert.True(okResult.Value != null);
                if (okResult.Value is CacheDataDto obj)
                {
                    Assert.True(obj.CreateDate == startingCreateDate);
                }
                else
                    Assert.Fail("");
            }
            else
                Assert.Fail("");
        }
    }
}