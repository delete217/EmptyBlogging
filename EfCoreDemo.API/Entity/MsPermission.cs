using System;
using System.Collections.Generic;

namespace EfCoreDemo.API.Entity;

public partial class MsPermission
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public string Description { get; set; } = null!;
}
