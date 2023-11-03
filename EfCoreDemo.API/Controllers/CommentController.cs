using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Filters;
using EfCoreDemo.API.Service;
using EfCoreDemo.API.Vo;
using EfCoreDemo.API.Vo.common;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreDemo.API.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class CommentController : ControllerBase
{
    public ICommentService _commentService;
    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    [TypeFilter(typeof(CtmAuthorizationFilterAttribute))]
    public GlobalResult AddComments(PutCommentVo putCommentVo)
    {
        MsAdmin? curAdmin = HttpContext.Items["AdminInfo"] as MsAdmin;
        CommentsVo commentsVo = _commentService.AddComments(putCommentVo, curAdmin.Id);

        return GlobalResult.Success(commentsVo);

    }
}
