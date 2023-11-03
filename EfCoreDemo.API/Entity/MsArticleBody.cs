using System;
using System.Collections.Generic;

namespace EfCoreDemo.API.Entity;

public partial class MsArticleBody
{
    public long Id { get; set; }

    public string? Content { get; set; }

    public string? ContentHtml { get; set; }

    public long ArticleId { get; set; }
}
