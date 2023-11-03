using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EfCoreDemo.API.Entity;

public partial class MsAdmin
{
    public long Id { get; set; }

    [DefaultValue("lisi")]
    public string Username { get; set; } = null!;

    [DefaultValue("123456")]
    public string Password { get; set; } = null!;

    public string Avatar { get; set; }
}
