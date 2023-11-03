using EfCoreDemo.API.Entity;

namespace EfCoreDemo.API.Service;

public interface IAdminService
{
    MsAdmin GetUserByUserName(string username);
}
