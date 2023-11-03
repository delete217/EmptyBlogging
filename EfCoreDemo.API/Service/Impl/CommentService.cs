using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Vo;
using Microsoft.Extensions.Caching.Distributed;

namespace EfCoreDemo.API.Service.Impl;

public class CommentService : ICommentService
{
    private BlogContext _blogContext;
    public IArticleService _articleService;
    public IUserService _userService;
    public CommentService(BlogContext blogContext, IArticleService articleService, IUserService userService)
    {
        _blogContext = blogContext;
        _articleService = articleService;
        _userService = userService;
    }

    // 添加一条评论
    public CommentsVo AddComments(PutCommentVo putCommentVo, long authorId)
    {
        // 映射
        MsComment comment = new MsComment
        {
            Content = putCommentVo.content,
            CreateDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            ArticleId = putCommentVo.articleId,
            AuthorId = authorId,
            ParentId = putCommentVo.parentId,
            Level = putCommentVo.lever
        };
        // 获取被评论者Id
        long toUid = (long)_articleService.GetArticleById(comment.ArticleId).AuthorId;
        comment.ToUid = toUid;
        // 获取评论者姓名
        string commentName = _userService.GetUserNameById(authorId);
        CommentsVo commentsVo = CommentsVo.convertToCommentsVo(comment, commentName);
        _blogContext.MsComments.Add(comment);
        _blogContext.SaveChanges();

        return commentsVo;
    }



    // 通过文章Id获取所有评论
    public List<MsComment> GetAllCommentsByArticleId(long articleId)
    {
        return _blogContext.MsComments.Where(c => c.ArticleId == articleId).ToList();
    }
}
