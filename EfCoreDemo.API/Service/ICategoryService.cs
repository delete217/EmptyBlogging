using EfCoreDemo.API.Entity;

namespace EfCoreDemo.API.Service;

public interface ICategoryService
{
    List<MsCategory> GetAllCategorys();
    string? GetCategoryNameById(long id);
}
