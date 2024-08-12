using Altin.Application.Models.Product;
using Altin.Web.Areas.Admin.Models;

namespace Altin.Application.Services;

public interface IProductService
{
    Task<GetProductModel> GetAsync(Guid id);
    
    Task<List<GetProductModel>> GetPopularProductsAsync();

    Task<List<GetProductModel>> GetAllAsync();

    Task AddAsync(ProductUploadModel model);

    Task UpdateAsync(ProductUpdateReq model);

    Task<ProductImageUpdateReturnModel> UpdateImageAsync(Guid id, string imageUrl);

    Task<ProductDeleteReturnModel> DeleteAsync(Guid id);
}