using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Vo;
using EfCoreDemo.API.Service;
using EfCoreDemo.API.Service.Impl;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using EfCoreDemo.API.Vo.common;
using EfCoreDemo.API.Filters;

namespace EfCoreDemo.API.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public bool AddUser(MsAdmin msAdmin)
    {
        return _userService.AddUser(msAdmin);
    }

    [HttpPost]
    [TypeFilter(typeof(CtmAuthorizationFilterAttribute))]
    [TypeFilter(typeof(CtmResourceFilterAttribute))]
    public GlobalResult NeedToken()
    {
        MsAdmin? curAdmin = HttpContext.Items["AdminInfo"] as MsAdmin;
        return GlobalResult.Success(curAdmin);
        //return GlobalResult.Success("===");
    }
    [HttpGet]
    [TypeFilter(typeof(CtmAuthorizationFilterAttribute))]
    public GlobalResult GetUserInfo()
    {
        // 前端传入的token对应的对象
        MsAdmin? curAdmin = HttpContext.Items["AdminInfo"] as MsAdmin;
        MsSysUser curUser = _userService.GetUserDetailById(curAdmin.Id);
        return GlobalResult.Success(curUser);
    }
}
