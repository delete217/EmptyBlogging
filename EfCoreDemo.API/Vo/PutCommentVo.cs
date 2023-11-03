namespace EfCoreDemo.API.Vo;

public class PutCommentVo
{
    public string content { get; set; }
    public long articleId { get; set; }
    public long parentId { get; set; }

    public string lever { get; set; }


}
