using System.CodeDom;
using EfCoreDemo.API.Entity;

namespace EfCoreDemo.API.utils;

public class UserThreadLocal
{
    public static MsAdmin userinfo { get; set; }

    public static MsAdmin GetLocalUserInfo()
    {
        return userinfo;
    }
    public static void SetLocalUserInfo(MsAdmin msAdmin)
    {
        userinfo = msAdmin;
    }
}
