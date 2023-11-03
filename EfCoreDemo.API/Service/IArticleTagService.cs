
namespace EfCoreDemo.API.Service;

public interface IArticleTagService
{
    List<long> AddArticleAndTag(long articleId, List<long> tagIds);
    List<long> GetArticleIdsByTagId(long tagId);
}
