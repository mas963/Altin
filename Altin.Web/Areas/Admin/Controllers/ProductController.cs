﻿using Altin.Application.Exceptions;
using Altin.Application.Models.Product;
using Altin.Application.Services;
using Altin.Web.Areas.Admin.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Altin.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ProductController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IWebHostEnvironment webHostEnvironment, IProductService productService,
        ICategoryService categoryService)
    {
        _webHostEnvironment = webHostEnvironment;
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var products = await _productService.GetAllAsync(page: page);

        return View(products);
    }

    public async Task<IActionResult> Add()
    {
        var categories = await _categoryService.GetAllAsync();

        ViewData["Categories"] = categories;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload([FromForm] ProductUploadViewModel model,
        [FromServices] IValidator<ProductUploadViewModel> validator)
    {
        var validationResult = validator.Validate(model);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return BadRequest(new { message = errors });
        }

        var permittedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(model.ProductImage.FileName).ToLowerInvariant();

        if (!permittedExtensions.Contains(extension))
        {
            return BadRequest(new { message = "Sadece .jpg, .jpeg, .png uzantılı dosyalar yüklenebilir." });
        }

        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img/Products");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductName + extension;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        try
        {
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.ProductImage.CopyToAsync(fileStream);
            }

            var product = new ProductUploadModel
            {
                ProductName = model.ProductName,
                ProductDescription = model.ProductDescription,
                ProductImageName = uniqueFileName,
                HepsiburadaUrl = model.HepsiburadaUrl,
                TrendyolUrl = model.TrendyolUrl,
                Price = model.Price,
                IsPopularProduct = model.IsPopularProduct,
                CategoryIds = model.CategoryIds
            };

            await _productService.AddAsync(product);

            Thread.Sleep(2000); // TODO: delete

            return Ok(new { message = "Ürün başarıyla eklendi" });
        }
        catch (BadRequestException ex)
        {
            // Eğer bir hata oluşursa yüklenen resmi sil
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return BadRequest(new { message = "Ürün eklenirken bir hata oluştu: " + ex.Message });
        }
    }

    public async Task<IActionResult> Update(Guid id)
    {
        try
        {
            var product = await _productService.GetUpdateDetailAsync(id);
            ViewData["CategoriesToProduct"] = await _categoryService.GetAllWithSelectedAsync(id);
            return View(product);
        }
        catch (NotFoundException ex)
        {
            return NotFound();
        }
    }

    [HttpPut]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] ProductUpdateReq model,
        [FromServices] IValidator<ProductUpdateReq> validator)
    {
        var validationResult = validator.Validate(model);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return BadRequest(new { message = errors });
        }

        try
        {
            await _productService.UpdateAsync(model);

            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateImage([FromForm] ProductImageUpdateViewModel model)
    {
        try
        {
            if (model.NewProductImage == null)
            {
                return Json(new { success = false, message = "Lütfen bir dosya seçin." });
            }

            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(model.NewProductImage.FileName).ToLowerInvariant();

            if (!permittedExtensions.Contains(extension))
            {
                return Json(new
                    { success = false, message = "Sadece .jpg, .jpeg, .png uzantılı dosyalar yüklenebilir." });
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img/Products");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var product = await _productService.GetAsync(model.ProductId);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + product.Name + extension;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.NewProductImage.CopyToAsync(fileStream);
            }

            var updatedProduct = await _productService.UpdateImageAsync(model.ProductId, uniqueFileName);

            // Eski resmi silme
            if (!string.IsNullOrEmpty(updatedProduct.OldImageName))
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Products",
                    updatedProduct.OldImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            Thread.Sleep(3000); // TODO: delete

            return Json(new { success = true, newImageUrl = uniqueFileName });
        }
        catch (NotFoundException ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var productDeleteReturnModel = await _productService.DeleteAsync(id);

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Products",
                productDeleteReturnModel.ProductImage);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            return Json(new { success = true });
        }
        catch (NotFoundException)
        {
            return Json(new { success = false });
        }
    }
}