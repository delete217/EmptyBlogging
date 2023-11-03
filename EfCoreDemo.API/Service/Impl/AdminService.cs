using EfCoreDemo.API.Entity;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDemo.API.Service.Impl;

public class AdminService : IAdminService
{
    public BlogContext _blogContext;
    public AdminService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }
    public MsAdmin GetUserByUserName(string username)
    {
        MsAdmin? msAdmin = _blogContext.MsAdmins.FirstOrDefault(a => a.Username == username);
        return msAdmin;
    }
}
