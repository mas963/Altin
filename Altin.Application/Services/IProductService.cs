using Altin.Application.Models.Product;

namespace Altin.Application.Services;

public interface IProductService
{
    Task<GetProductModel> GetAsync(Guid id);

    Task<GetProductDetailModel> GetBySlugAsync(string slug);
    
    Task<List<GetProductModel>> GetPopularProductsAsync();

    Task<GetProductWithPagination> GetAllAsync(int page = 1, int pageSize = 9);

    Task<GetProductWithPagination> GetAllByCategoryAsync(string categoryNormalizeName, int page = 1,
        int size = 9);

    Task AddAsync(ProductUploadModel model);

    Task UpdateAsync(ProductUpdateReq model);

    Task<ProductImageUpdateReturnModel> UpdateImageAsync(Guid id, string imageUrl);

    Task<ProductDeleteReturnModel> DeleteAsync(Guid id);

    Task<List<GetProductModel>> GetSimilarProductsAsync(Guid productId);
}