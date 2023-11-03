namespace EfCoreDemo.API.Vo;

public class ArticleVo
{
    public long ArticleId { get; set; }
    public string? AuthorName { get; set; }
    public string? CategoryName { get; set; }
    public long? CreateTime { get; set; }
    public string? Title { get; set; }
    public int ViewCounts { get; set; }
    public string? Summary { get; set; }

    public ArticleVo(long articleId, string authorName, string categoryName, long createTime, string title, int viewCounts, string summary)
    {
        ArticleId = articleId;
        AuthorName = authorName;
        CategoryName = categoryName;
        CreateTime = createTime;
        Title = title;
        ViewCounts = viewCounts;
        Summary = summary;
    }

    public ArticleVo() { }
}
