using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Service;
using EfCoreDemo.API.Vo;
using EfCoreDemo.API.Vo.common;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreDemo.API.Controllers;

[ApiController]
[Route("/api")]
public class LoginController
{
    private ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }
    [HttpPost("Login")]
    public GlobalResult Login(LoginVo loginVo)
    {

        return _loginService.Login(loginVo);
    }
}
