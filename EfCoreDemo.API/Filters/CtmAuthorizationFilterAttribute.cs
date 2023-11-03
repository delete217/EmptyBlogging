using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Vo.common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace EfCoreDemo.API.Filters;

public class CtmAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
{
    public IDistributedCache _distributedCache;
    public BlogContext _blogContext;
    public CtmAuthorizationFilterAttribute(IDistributedCache distributedCache, BlogContext blogContext)
    {
        _distributedCache = distributedCache;
        _blogContext = blogContext;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var header = context.HttpContext.Request.Headers;

        var authorization = header.ContainsKey("Authorization");
        if (authorization)
        {
            var userToken = header["Authorization"];
            // 如果redis存在token
            var userinfo = _distributedCache.GetString("_Token_" + userToken);
            if (userinfo == null)
            {
                context.Result = new ObjectResult(GlobalResult.Fail((int)ErrorCode.TOKEN_NOT_LEGAL, ErrorCodeExtensions.GetMessage(ErrorCode.TOKEN_NOT_LEGAL)));
            }
            else
            {
                // Token校验成功，将用户信息存储到 HttpContext.Items
                MsAdmin msAdmin = JsonConvert.DeserializeObject<MsAdmin>(userinfo);
                context.HttpContext.Items["AdminInfo"] = msAdmin;
            }

        }
        else
        {
            context.Result = new ObjectResult(GlobalResult.Fail((int)ErrorCode.NOT_LOGIN, ErrorCodeExtensions.GetMessage(ErrorCode.NOT_LOGIN)));
        }

        //throw new NotImplementedException();
    }
}
