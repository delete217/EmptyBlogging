using EfCoreDemo.API.Entity;

namespace EfCoreDemo.API.Vo;

public class CommentsVo
{
    public long Id { get; set; }

    public string Content { get; set; } = null!;

    public long CreateDate { get; set; }

    public string AuthorName { get; set; }

    public long parentId { get; set; }
    public string Level { get; set; } = null!;
    public List<CommentsVo> childComments { get; set; }

    public static CommentsVo convertToCommentsVo(MsComment msComment, string authorName)
    {
        // string authorName = _userService.GetUserNameById(msComment.AuthorId);
        CommentsVo commentsVo = new CommentsVo
        {
            Id = msComment.Id,
            Content = msComment.Content,
            CreateDate = msComment.CreateDate,
            Level = msComment.Level,
            AuthorName = authorName,
            parentId = msComment.ParentId
        };
        return commentsVo;
    }
}
