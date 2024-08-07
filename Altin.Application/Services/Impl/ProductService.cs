using Altin.Application.Models.Product;
using Altin.Core.Entities;
using Altin.DataAccess.Repositories;

namespace Altin.Application.Services.Impl;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<GetProductModel>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(x => new GetProductModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            ImageUrl = x.ImageUrl
        }).ToList();
    }

    public async Task AddAsync(ProductUploadModel model)
    {
        var product = new Product
        {
            Name = model.ProductName,
            Description = model.ProductDescription,
            ImageUrl = model.ProductImageName
        };

        await _productRepository.AddAsync(product);
    }
}