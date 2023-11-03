using System.Text;
using EfCoreDemo.API.Vo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace EfCoreDemo.API.Filters;

//配置Redis缓存
public class CtmResourceFilterAttribute : Attribute, IResourceFilter
{
    private readonly IDistributedCache distributedCache;
    private readonly ILogger<CtmResourceFilterAttribute> logger;

    public CtmResourceFilterAttribute(IDistributedCache distributedCache, ILogger<CtmResourceFilterAttribute> logger)
    {
        this.distributedCache = distributedCache;
        this.logger = logger;
    }
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        //获取当前访问路径
        PathString path = context.HttpContext.Request.Path;
        ObjectResult result = context.Result as ObjectResult;
        Result myResult = result.Value as Result;
        if (myResult != null)
        {
            // 转换 Result 对象为字节数组
            string serializedResult = JsonConvert.SerializeObject(myResult);

            // 设置缓存数据，可以设置缓存过期时间等选项
            distributedCache.SetString(path, serializedResult, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
            logger.LogInformation("～～～～～存入缓存～～～～～");
        }
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        string path = context.HttpContext.Request.Path;
        string cachedData = distributedCache.GetString(path);
        if (cachedData != null)
        {
            // 从缓存中获取到数据并进行反序列化
            Result deserializedResult = JsonConvert.DeserializeObject<Result>(cachedData);

            //Result deserializedResult = JsonSerializer.Deserialize<Result>(jsonData);
            // 使用反序列化后的对象设置结果
            context.Result = new ObjectResult(deserializedResult);
            logger.LogInformation("～～～～～走了缓存～～～～～");
        }

    }
}
