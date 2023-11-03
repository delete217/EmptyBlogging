using EfCoreDemo.API.Entity;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDemo.API.Service.Impl;

public class ArticleService : IArticleService
{
    private BlogContext _blogContext;

    public ArticleService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public long AddNewArticle(MsArticle msArticle)
    {
        _blogContext.MsArticles.Add(msArticle);
        _blogContext.SaveChanges();
        return msArticle.Id;
    }

    public List<MsArticle> GetAllArticles()
    {
        return _blogContext.MsArticles.ToList();
    }

    public List<MsArticle> GetArticleByCategoryId(long categoryId)
    {
        return _blogContext.MsArticles.Where(a => a.CategoryId == categoryId).ToList();
    }

    public MsArticle GetArticleById(long id)
    {
        return _blogContext.MsArticles.FirstOrDefault(i => i.Id == id);
    }

    public List<MsArticle> GetArticlesByName(string articleName)
    {
        return _blogContext.MsArticles.Where(a => EF.Functions.Like(a.Title, $"%{articleName}%"))
        .ToList();

    }

    // 更新对象数据
    public void updateArticle(MsArticle msArticle)
    {
        var exist = _blogContext.MsArticles.Find(msArticle.Id);
        if (exist != null)
        {
            exist.BodyId = msArticle.BodyId;
            _blogContext.SaveChanges();
        }
    }
}
