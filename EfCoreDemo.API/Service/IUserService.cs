using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Vo;
namespace EfCoreDemo.API.Service;

public interface IUserService
{
    bool AddUser(MsAdmin msAdmin);
    MsSysUser GetUserDetailById(long id);
    string? GetUserNameById(long id);
}
