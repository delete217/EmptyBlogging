namespace EfCoreDemo.API.Vo;

public class PutArticleVo
{
    public string? Summary { get; set; }

    public string? Title { get; set; }

    public long? CategoryId { get; set; }

    public List<long> TagIds { get; set; }

    public string? mdContent { get; set; }

    public string? htmlContent { get; set; }

    public long? CreateTime { get; set; }

}
