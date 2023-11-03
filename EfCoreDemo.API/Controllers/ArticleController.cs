using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Vo;
using EfCoreDemo.API.Service;
using EfCoreDemo.API.Service.Impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Forms;
using EfCoreDemo.API.Filters;
using EfCoreDemo.API.Vo.common;
using System.Net.NetworkInformation;
namespace EfCoreDemo.API.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class ArticleController : ControllerBase
{

    private IArticleService _arcticleService;
    private IArcticleBodyService _arcticleBodyService;
    private IUserService _userService;
    private ICategoryService _categoryService;
    private ICommentService _commentService;
    private IArticleTagService _articleTagService;
    public ArticleController(IArticleService arcticleService, IArcticleBodyService arcticleBodyService,
    IUserService userService, ICategoryService categoryService, ICommentService commentService
    , IArticleTagService articleTagService)
    {
        _arcticleService = arcticleService;
        _arcticleBodyService = arcticleBodyService;
        _userService = userService;
        _categoryService = categoryService;
        _commentService = commentService;
        _articleTagService = articleTagService;
    }

    [HttpGet]
    public List<MsArticle> GetAllArticles()
    {
        return _arcticleService.GetAllArticles();
    }
    [HttpGet]
    public List<MsArticleBody> GetAllArticleBodies()
    {
        return _arcticleBodyService.GetAllArticleBodies();
    }

    // 首页分页显示所有文章
    [TypeFilter(typeof(CtmResourceFilterAttribute))]
    [HttpGet]
    public Result GetArticleVos()
    {
        List<MsArticle> articles = _arcticleService.GetAllArticles();
        List<ArticleVo> articleVos = new List<ArticleVo>(articles.Count);
        for (int i = 0; i < articles.Count; i++)
        {
            ArticleVo articleVo = new ArticleVo
            {
                //根据Article为ArticleVo需要的值 赋值
                ArticleId = articles[i].Id,
                Summary = articles[i].Summary,
                Title = articles[i].Title,
                CreateTime = articles[i].CreateDate,
                ViewCounts = (int)articles[i].ViewCounts
            };

            long? AuthorId = articles[i].AuthorId;// ?
            long? CategoryId = articles[i].CategoryId;
            string? UserName = _userService.GetUserNameById((long)AuthorId);
            string? CategoryName = _categoryService.GetCategoryNameById((long)CategoryId);
            articleVo.AuthorName = UserName;
            articleVo.CategoryName = CategoryName;

            articleVos.Add(articleVo);
        }

        return Result.Success(articleVos);
    }
    [HttpGet]
    public GlobalResult GetArticleVosByName(string articleName)
    {
        List<MsArticle> articles = _arcticleService.GetArticlesByName(articleName);
        List<ArticleVo> articleVos = new List<ArticleVo>(articles.Count);
        for (int i = 0; i < articles.Count; i++)
        {
            ArticleVo articleVo = new ArticleVo
            {
                //根据Article为ArticleVo需要的值 赋值
                ArticleId = articles[i].Id,
                Summary = articles[i].Summary,
                Title = articles[i].Title,
                CreateTime = articles[i].CreateDate,
                ViewCounts = (int)articles[i].ViewCounts
            };

            long? AuthorId = articles[i].AuthorId;// ?
            long? CategoryId = articles[i].CategoryId;
            string? UserName = _userService.GetUserNameById((long)AuthorId);
            string? CategoryName = _categoryService.GetCategoryNameById((long)CategoryId);
            articleVo.AuthorName = UserName;
            articleVo.CategoryName = CategoryName;

            articleVos.Add(articleVo);
        }

        // return Result.Success(articleVos);
        return GlobalResult.Success(articleVos);
    }

    // 查看文章详情
    [HttpGet]
    public GlobalResult View(long id)
    {
        // 获取当前文章id, 通过文章id 查询作者，摘要，内容等信息
        MsArticle msArticle = _arcticleService.GetArticleById(id);
        MsArticleBody articleBody = _arcticleBodyService.GetArticleBodyById(id);

        // 获取当前文章所有评论
        List<MsComment> msComments = _commentService.GetAllCommentsByArticleId(id);
        List<MsComment> ftComments = new List<MsComment>(msComments.Count);
        List<MsComment> cdComments = new List<MsComment>(msComments.Count);
        // 把获取到的评论分为父子两个列表
        for (int i = 0; i < msComments.Count; i++)
        {
            if (msComments[i].ParentId != 0)
            {
                cdComments.Add(msComments[i]);
            }
            else
            {
                ftComments.Add(msComments[i]);
            }
        }
        // 重新封装comment => commentVo  (按照父子评论进行封装)
        List<CommentsVo> commentsVos = new List<CommentsVo>(msComments.Count);
        // 遍历所有父评论
        for (int i = 0; i < ftComments.Count; i++)
        {
            string authorName1 = _userService.GetUserNameById(ftComments[i].AuthorId);
            CommentsVo ftCommentsVo = CommentsVo.convertToCommentsVo(ftComments[i], authorName1);
            List<CommentsVo> childComments = new List<CommentsVo>(cdComments.Count);
            ftCommentsVo.childComments = childComments;
            // 查找子评论
            for (int j = 0; j < cdComments.Count; j++)
            {
                if (cdComments[j].ParentId == ftComments[i].Id)
                {
                    string authorName2 = _userService.GetUserNameById(cdComments[j].AuthorId);
                    CommentsVo cdCommentsVo = CommentsVo.convertToCommentsVo(cdComments[j], authorName2);
                    ftCommentsVo.childComments.Add(cdCommentsVo);
                }
            }
            commentsVos.Add(ftCommentsVo);
        }
        string AuthorName = _userService.GetUserNameById((long)msArticle.AuthorId);
        ArticleDetailsVo articleDetailsVo = new ArticleDetailsVo
        {
            Title = msArticle.Title,
            ViewCounts = (int)msArticle.ViewCounts,
            CreateTime = (long)msArticle.CreateDate,
            Content = articleBody.ContentHtml,
            Author = AuthorName,
            Summary = msArticle.Summary,
            comments = commentsVos
        };
        if (articleDetailsVo != null)
        {
            return GlobalResult.Success(articleDetailsVo);
        }
        else
        {
            return GlobalResult.Fail((int)ErrorCode.PARAMS_ERROR, ErrorCodeExtensions.GetMessage(ErrorCode.PARAMS_ERROR));
        }
    }

    // 通过tagId获取文章列表
    [HttpGet]
    public GlobalResult GetArticlesByTagId(long tagId)
    {
        // 获取文章id列表
        List<long> articleIds = _articleTagService.GetArticleIdsByTagId(tagId);

        // 根据文章id获取文章
        // MsArticle => ArticleVo
        List<ArticleTagVo> articleTagVos = new List<ArticleTagVo>(articleIds.Count);
        foreach (long articleId in articleIds)
        {
            MsArticle msArticle = _arcticleService.GetArticleById(articleId);
            ArticleTagVo articleTagVo = new ArticleTagVo
            {
                ArticleId = msArticle.Id,
                Title = msArticle.Title,
                CreateTime = msArticle.CreateDate,
            };
            string authorName = _userService.GetUserNameById((long)msArticle.AuthorId);
            articleTagVo.AuthorName = authorName;
            articleTagVos.Add(articleTagVo);
        }

        return GlobalResult.Success(articleTagVos);
    }

    // 通过categoryId获取文章列表
    [HttpGet]
    public GlobalResult GetArticlesByCategoryId(long categoryId)
    {
        List<MsArticle> articles = _arcticleService.GetArticleByCategoryId(categoryId);
        string categoryName = _categoryService.GetCategoryNameById(categoryId);
        List<ArticleCategoryVo> articleCategoryVos = new List<ArticleCategoryVo>(articles.Count);
        for (int i = 0; i < articles.Count; i++)
        {
            MsArticle msArticle = articles[i];
            ArticleCategoryVo articleCategoryVo = new ArticleCategoryVo
            {
                ArticleId = msArticle.Id,
                Title = msArticle.Title,
                CreateTime = msArticle.CreateDate,
                CategoryName = categoryName,
            };
            string authorName = _userService.GetUserNameById((long)msArticle.AuthorId);
            articleCategoryVo.AuthorName = authorName;
            articleCategoryVos.Add(articleCategoryVo);
        }

        return GlobalResult.Success(articleCategoryVos);
    }

    // 发布新文章
    [HttpPost]
    [TypeFilter(typeof(CtmAuthorizationFilterAttribute))]
    public GlobalResult ReleaseArticle(PutArticleVo putArticleVo)
    {
        // Token获取数据
        MsAdmin? curAdmin = HttpContext.Items["AdminInfo"] as MsAdmin;
        // 添加到 MsArticle表
        MsArticle msArticle = new MsArticle
        {
            CommentCounts = 0,
            CreateDate = putArticleVo.CreateTime,
            Summary = putArticleVo.Summary,
            Title = putArticleVo.Title,
            ViewCounts = 0,
            Weight = 0,
            AuthorId = curAdmin.Id,
            CategoryId = putArticleVo.CategoryId
        };

        // 添加数据到 MsArticle表，并获取新的articleId
        long articleId = _arcticleService.AddNewArticle(msArticle);
        // 添加到 MsArticleBody表
        MsArticleBody msArticleBody = new MsArticleBody
        {
            Content = putArticleVo.mdContent,
            ContentHtml = putArticleVo.htmlContent,
            ArticleId = articleId
        };
        long articleBodyId = _arcticleBodyService.AddArticleBody(msArticleBody);
        msArticle.BodyId = articleBodyId;
        _arcticleService.updateArticle(msArticle);
        // 添加到 Arcticle-Tag表
        List<long> articleTagIds = _articleTagService.AddArticleAndTag(articleId, putArticleVo.TagIds);

        // 封装最终结果
        ReleaseArticleSuccessVo successVo = new ReleaseArticleSuccessVo
        {
            articleBodyId = articleBodyId,
            articleId = articleId,
            articleTagIds = articleTagIds
        };
        if (successVo != null)
        {
            return GlobalResult.Success(successVo);
        }
        else
        {
            return GlobalResult.Fail((int)ErrorCode.PARAMS_ERROR, ErrorCodeExtensions.GetMessage(ErrorCode.PARAMS_ERROR));
        }

    }

}
