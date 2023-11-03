using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Vo;
using EfCoreDemo.API.Vo.common;

namespace EfCoreDemo.API.Service;

public interface ILoginService
{
    GlobalResult Login(LoginVo loginVo);
}
