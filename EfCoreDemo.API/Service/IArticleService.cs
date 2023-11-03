using EfCoreDemo.API.Entity;

namespace EfCoreDemo.API.Service;

public interface IArticleService
{
    long AddNewArticle(MsArticle msArticle);
    List<MsArticle> GetAllArticles();
    List<MsArticle> GetArticleByCategoryId(long categoryId);
    MsArticle GetArticleById(long id);
    List<MsArticle> GetArticlesByName(string articleName);
    void updateArticle(MsArticle msArticle);
}
