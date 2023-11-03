using System.Security.Claims;
using EfCoreDemo.API.Entity;
using EfCoreDemo.API.utils;
using EfCoreDemo.API.Vo;
using EfCoreDemo.API.Vo.common;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace EfCoreDemo.API.Service.Impl;

public class LoginService : ILoginService
{
    private IAdminService _adminService;
    private IDistributedCache _distributedCache;
    public LoginService(IAdminService adminService, IDistributedCache distributedCache)
    {
        _adminService = adminService;
        _distributedCache = distributedCache;
    }
    public GlobalResult Login(LoginVo loginVo)
    {
        // 先查找是否有该用户存在
        MsAdmin msAdmin = _adminService.GetUserByUserName(loginVo.Username);
        // 存在的话验证密码是否正确
        if (msAdmin != null)
        {
            // 用户信息存入用户线程
            UserThreadLocal.SetLocalUserInfo(msAdmin);
            if (msAdmin.Password == loginVo.Password)
            {
                // 生成Jwt负载
                List<Claim> claims = new List<Claim>(){
                    new Claim(ClaimTypes.NameIdentifier, msAdmin.Username),
                    new Claim(ClaimTypes.Role,msAdmin.Id.ToString()),
                };
                string token = JwtUtils.CreateToken(claims);
                _distributedCache.SetString("_Token_" + token, JsonConvert.SerializeObject(msAdmin));
                return GlobalResult.Success(msAdmin.Id + " " + msAdmin.Avatar + " " + JwtUtils.CreateToken(claims));
            }
            else
            {
                return GlobalResult.Fail((int)ErrorCode.PWD_NOT_CORRECT, ErrorCodeExtensions.GetMessage(ErrorCode.PWD_NOT_CORRECT));
            }
        }
        return GlobalResult.Fail((int)ErrorCode.PARAMS_ERROR, ErrorCodeExtensions.GetMessage(ErrorCode.PARAMS_ERROR));
    }
}
