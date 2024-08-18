using Altin.Application.Exceptions;
using Altin.Application.Models.News;
using Altin.Application.Services;
using Altin.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Altin.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class NewsController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService, IWebHostEnvironment webHostEnvironment)
    {
        _newsService = newsService;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] CreateNewsViewModel model)
    {
        if (model.Image == null)
        {
            return BadRequest(new {message = "Lütfen bir dosya seçin." });
        }

        var permittedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(model.Image.FileName).ToLowerInvariant();

        if (!permittedExtensions.Contains(extension))
        {
            return BadRequest(new { message = "Sadece .jpg, .jpeg, .png uzantılı dosyalar yüklenebilir." });
        }

        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img/News");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Title + extension;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        try
        {
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(fileStream);
            }

            var news = new CreateNewsModel()
            {
                Title = model.Title,
                Content = model.Content,
                Image = uniqueFileName
            };

            await _newsService.CreateAsync(news);

            Thread.Sleep(2000); // TODO: delete

            return Ok(new { message = "Haber & Duyuru başarıyla eklendi" });
        }
        catch (BadRequestException ex)
        {
            // Eğer bir hata oluşursa yüklenen resmi sil
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return BadRequest(new { message = "Haber yüklenirken bir hata oluştu: " + ex.Message });
        }
    }
}