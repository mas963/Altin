using Altin.Application.Models.Product;

namespace Altin.Application.Services;

public interface IProductService
{
    Task<List<GetProductModel>> GetAllAsync();

    Task AddAsync(ProductUploadModel model);
}