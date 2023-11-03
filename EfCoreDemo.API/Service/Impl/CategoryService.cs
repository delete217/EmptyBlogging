using EfCoreDemo.API.Entity;

namespace EfCoreDemo.API.Service.Impl;

public class CategoryService : ICategoryService
{
    private BlogContext _blogContext;
    public CategoryService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public List<MsCategory> GetAllCategorys()
    {
        return _blogContext.MsCategories.ToList();
    }

    public string GetCategoryNameById(long id)
    {
        MsCategory msCategory = _blogContext.MsCategories.FirstOrDefault(c => c.Id == id);
        return msCategory.CategoryName;
    }
}
