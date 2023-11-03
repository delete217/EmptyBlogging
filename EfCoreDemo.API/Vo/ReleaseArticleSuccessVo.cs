namespace EfCoreDemo.API.Vo;

public class ReleaseArticleSuccessVo
{
    public long articleId { get; set; }
    public long articleBodyId { get; set; }
    public List<long> articleTagIds { get; set; }
}
