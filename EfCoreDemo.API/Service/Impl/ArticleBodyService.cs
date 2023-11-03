using EfCoreDemo.API.Entity;

namespace EfCoreDemo.API.Service.Impl;

public class ArticleBodyService : IArcticleBodyService
{
    private BlogContext _blogContext;

    public ArticleBodyService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public long AddArticleBody(MsArticleBody msArticleBody)
    {
        _blogContext.MsArticleBodies.Add(msArticleBody);
        _blogContext.SaveChanges();
        return msArticleBody.ArticleId;
    }

    public List<MsArticleBody> GetAllArticleBodies()
    {
        return _blogContext.MsArticleBodies.ToList();
    }

    public MsArticleBody GetArticleBodyById(long id)
    {
        return _blogContext.MsArticleBodies.FirstOrDefault(i => i.ArticleId == id);
    }
}
