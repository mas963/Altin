using Altin.Application.Exceptions;
using Altin.Application.Models.Category;
using Altin.Core.Entities;
using Altin.DataAccess.Repositories;

namespace Altin.Application.Services.Impl;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<GetCategoryModel>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(x => new GetCategoryModel
        {
            Id = x.Id,
            Name = x.Name,
            CategoryOrder = x.CategoryOrder
        }).ToList();
    }

    public async Task CreateAsync(CreateCategoryModel model)
    {
        var isExist = await _categoryRepository.AnyAsync(x => x.Name == model.Name);
        if (isExist)
        {
            throw new BadRequestException("Bu kategori zaten var");
        }

        var category = new Category
        {
            Name = model.Name,
            CategoryOrder = model.CategoryOrder
        };

        await _categoryRepository.AddAsync(category);
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.GetAsync(x => x.Id == id);
        if (category == null)
        {
            throw new NotFoundException("Kategori bulunamadı");
        }

        await _categoryRepository.DeleteAsync(category);
    }
}