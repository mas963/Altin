using Altin.Application.Exceptions;
using Altin.Application.Models.Category;
using Altin.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Altin.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();

        return View(categories);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromBody] CreateCategoryModel model)
    {
        try
        {
            await _categoryService.CreateAsync(model);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _categoryService.DeleteAsync(id);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }
}