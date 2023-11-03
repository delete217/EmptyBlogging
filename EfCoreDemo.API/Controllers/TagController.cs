using EfCoreDemo.API.Service;
using EfCoreDemo.API.Service.Impl;
using EfCoreDemo.API.Vo.common;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreDemo.API.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class TagController
{
    private ITagService _tagService;
    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public GlobalResult GetAllTags()
    {
        return _tagService.GetAllTags();
    }
}
