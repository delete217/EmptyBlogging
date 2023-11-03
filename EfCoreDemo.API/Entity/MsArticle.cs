using System;
using System.Collections.Generic;

namespace EfCoreDemo.API.Entity;

public partial class MsArticle
{
    public long Id { get; set; }

    /// <summary>
    /// 评论数量
    /// </summary>
    public int? CommentCounts { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public long? CreateDate { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 浏览数量
    /// </summary>
    public int? ViewCounts { get; set; }

    /// <summary>
    /// 是否置顶
    /// </summary>
    public int Weight { get; set; }

    /// <summary>
    /// 作者id
    /// </summary>
    public long? AuthorId { get; set; }

    /// <summary>
    /// 内容id
    /// </summary>
    public long? BodyId { get; set; }

    /// <summary>
    /// 类别id
    /// </summary>
    public long? CategoryId { get; set; }
}
