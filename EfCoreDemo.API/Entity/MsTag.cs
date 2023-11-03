using System;
using System.Collections.Generic;

namespace EfCoreDemo.API.Entity;

public partial class MsTag
{
    public long Id { get; set; }

    public string? Avatar { get; set; }

    public string? TagName { get; set; }
}
