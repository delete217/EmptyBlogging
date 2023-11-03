using System;
using System.Collections.Generic;

namespace EfCoreDemo.API.Entity;

public partial class MsCategory
{
    public long Id { get; set; }

    public string? Avatar { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }
}
