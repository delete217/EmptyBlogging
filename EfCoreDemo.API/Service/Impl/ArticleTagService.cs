
using EfCoreDemo.API.Entity;

namespace EfCoreDemo.API.Service.Impl;

public class ArticleTagService : IArticleTagService
{
    public BlogContext _blogContext;
    public ArticleTagService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public List<long> AddArticleAndTag(long articleId, List<long> tagIds)
    {
        List<long> articleTagIds = new List<long>(10);
        foreach (long tagId in tagIds)
        {
            MsArticleTag msArticleTag = new MsArticleTag
            {
                ArticleId = articleId,
                TagId = tagId
            };
            _blogContext.MsArticleTags.Add(msArticleTag);
            _blogContext.SaveChanges();
            articleTagIds.Add(msArticleTag.Id);
        }
        return articleTagIds;
    }

    public List<long> GetArticleIdsByTagId(long tagId)
    {
        List<MsArticleTag> msArticleTags = _blogContext.MsArticleTags.Where(i => i.TagId == tagId)
        .ToList();
        List<long> articleIds = new List<long>(msArticleTags.Count);
        foreach (MsArticleTag item in msArticleTags)
        {
            articleIds.Add(item.ArticleId);
        }
        return articleIds;
    }
}
