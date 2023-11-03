using EfCoreDemo.API.Entity;

namespace EfCoreDemo.API.Service;

public interface IArcticleBodyService
{
    long AddArticleBody(MsArticleBody msArticleBody);
    List<MsArticleBody> GetAllArticleBodies();
    MsArticleBody GetArticleBodyById(long id);
}
