using System.Text.Json;
using System.Text.Json.Serialization;
using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Vo;
using Microsoft.EntityFrameworkCore;
namespace EfCoreDemo.API.Service.Impl;

public class UserService : IUserService
{
    private BlogContext _blogContext;

    public UserService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public bool AddUser(MsAdmin msAdmin)
    {
        try
        {
            _blogContext.MsAdmins.Add(msAdmin);

            // 全局数据取消数据跟踪
            _blogContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            // 局部数据关闭实体追踪，提高性能
            // 没有实体追踪时需要contex.Update()保存数据库操作
            var user = _blogContext.MsAdmins.AsNoTracking().FirstOrDefault(u => u.Id == msAdmin.Id + 1);
            user.Username = "admin1";

            _blogContext.Update(user);
            _blogContext.SaveChanges();
            return true; // 如果成功保存更改，返回 true
        }
        catch (Exception ex)
        {
            // 在实际应用中，可以记录异常信息，进行适当的处理
            Console.WriteLine($"Error adding user: {ex.Message}");
            return false; // 如果保存更改时发生异常，返回 false
        }
    }

    public List<UserVo> GetUserById(int id)
    {
        List<UserVo> userVos = _blogContext.MsAdmins.
        Where(user => user.Id == id)
        .Select(user => new UserVo
        {
            Username = user.Username,
            Password = user.Password
        })
        .ToList();

        return userVos;
    }

    public MsSysUser GetUserDetailById(long id)
    {
        MsSysUser sysUser = _blogContext.MsSysUsers.FirstOrDefault(s => s.Id == id);
        return sysUser;
    }



    public string GetUserNameById(long id)
    {
        MsSysUser sysUser = _blogContext.MsSysUsers.FirstOrDefault(m => m.Id == id);
        return sysUser.Nickname;
    }
}
