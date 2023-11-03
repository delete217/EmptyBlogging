using System;
using System.Collections.Generic;

namespace EfCoreDemo.API.Entity;

public partial class MsArticleTag
{
    public long Id { get; set; }

    public long ArticleId { get; set; }

    public long TagId { get; set; }
}
