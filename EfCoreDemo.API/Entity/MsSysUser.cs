using System;
using System.Collections.Generic;

namespace EfCoreDemo.API.Entity;

public partial class MsSysUser
{
    public long Id { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    public string? Account { get; set; }

    /// <summary>
    /// 是否管理员
    /// </summary>
    public ulong? Admin { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 注册时间
    /// </summary>
    public long? CreateDate { get; set; }

    /// <summary>
    /// 是否删除
    /// </summary>
    public ulong? Deleted { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 最后登录时间
    /// </summary>
    public long? LastLogin { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? MobilePhoneNumber { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string? Nickname { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 加密盐
    /// </summary>
    public string? Salt { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string? Status { get; set; }
}
