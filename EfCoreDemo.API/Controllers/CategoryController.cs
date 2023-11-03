using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Service;
using EfCoreDemo.API.Vo.common;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreDemo.API.Controllers;


[ApiController]
[Route("/api/[controller]/[action]")]
public class CategoryController
{
    private ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public GlobalResult GetAllCategory()
    {
        List<MsCategory> categories = _categoryService.GetAllCategorys();
        return GlobalResult.Success(categories);
    }
}
