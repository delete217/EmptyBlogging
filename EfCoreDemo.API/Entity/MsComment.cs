using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreDemo.API.Entity;

public partial class MsComment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string Content { get; set; } = null!;

    public long CreateDate { get; set; }

    public long ArticleId { get; set; }

    public long AuthorId { get; set; }

    public long ParentId { get; set; }

    public long ToUid { get; set; }

    public string Level { get; set; } = null!;
}
