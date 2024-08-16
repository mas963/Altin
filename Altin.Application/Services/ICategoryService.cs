using Altin.Application.Models.Category;

namespace Altin.Application.Services;

public interface ICategoryService
{
    Task<List<GetCategoryModel>> GetAllAsync();
    Task<List<GetCategoryWithSelectedModel>> GetAllWithSelectedAsync(Guid id);
    Task CreateAsync(CreateCategoryModel model);
    Task DeleteAsync(Guid id);
}