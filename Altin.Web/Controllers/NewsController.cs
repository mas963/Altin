using Altin.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Altin.Web.Controllers;

[Route("Haberler")]
public class NewsController : Controller
{
    private readonly INewsService _newsService;
    
    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var news = await _newsService.GetListAsync();
        
        return View(news);
    }
    
    [Route("Detay/{slug}")]
    public async Task<IActionResult> Detail(string slug)
    {
        var news = await _newsService.GetBySlugAsync(slug);
        
        return View(news);
    }
}