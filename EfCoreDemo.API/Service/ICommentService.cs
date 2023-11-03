using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Vo;

namespace EfCoreDemo.API.Service;

public interface ICommentService
{
    CommentsVo AddComments(PutCommentVo putCommentVo, long authorId);
    List<MsComment> GetAllCommentsByArticleId(long articleId);
}
