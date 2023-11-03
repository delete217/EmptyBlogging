using System;
using System.Collections.Generic;

namespace EfCoreDemo.API.Entity;

public partial class MsAdminPermission
{
    public long Id { get; set; }

    public long AdminId { get; set; }

    public long PermissionId { get; set; }
}
