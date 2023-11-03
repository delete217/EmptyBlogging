using System;
using System.Collections.Generic;

namespace EfCoreDemo.API.Entity;

public partial class MsSysLog
{
    public long Id { get; set; }

    public long? CreateDate { get; set; }

    public string? Ip { get; set; }

    public string? Method { get; set; }

    public string? Module { get; set; }

    public string? Nickname { get; set; }

    public string? Operation { get; set; }

    public string? Params { get; set; }

    public long? Time { get; set; }

    public long? Userid { get; set; }
}
