using System.Reflection.Metadata;
using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Vo.common;

namespace EfCoreDemo.API.Service.Impl;

public class TagService : ITagService
{
    private BlogContext _blogcontext;
    public TagService(BlogContext blogContext)
    {
        _blogcontext = blogContext;
    }
    public GlobalResult GetAllTags()
    {
        List<MsTag> msTags = _blogcontext.MsTags.ToList();

        return GlobalResult.Success(msTags);
    }
}
