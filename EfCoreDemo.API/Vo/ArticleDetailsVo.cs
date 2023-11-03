using EfCoreDemo.API.Entity;

namespace EfCoreDemo.API.Vo;

// 文章详情页
// 暂时缺少评论显示
public class ArticleDetailsVo
{
    public string Title { get; set; }
    public string Author { get; set; }
    public long CreateTime { get; set; }
    public string Content { get; set; }
    public int ViewCounts { get; set; }
    public string Summary { get; set; }
    public List<CommentsVo> comments { get; set; }
}
