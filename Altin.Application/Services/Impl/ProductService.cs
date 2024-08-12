using Altin.Application.Exceptions;
using Altin.Application.Models.Product;
using Altin.Core.Entities;
using Altin.DataAccess.Repositories;
using Altin.Web.Areas.Admin.Models;

namespace Altin.Application.Services.Impl;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetProductModel> GetAsync(Guid id)
    {
        var product = await _productRepository.GetAsync(x => x.Id == id);
        
        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }
        
        return new GetProductModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl
        };
    }

    public async Task<List<GetProductModel>> GetPopularProductsAsync()
    {
        var products = await _productRepository.GetAllAsync(x => x.IsPopular);
        
        return products.Select(x => new GetProductModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            ImageUrl = x.ImageUrl
        }).ToList();
    }

    public async Task<List<GetProductModel>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(x => new GetProductModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            ImageUrl = x.ImageUrl,
            IsPopular = x.IsPopular
        }).ToList();
    }

    public async Task AddAsync(ProductUploadModel model)
    {
        var product = new Product
        {
            Name = model.ProductName,
            Description = model.ProductDescription,
            ImageUrl = model.ProductImageName,
            IsPopular = model.IsPopularProduct
        };
        
        if (model.IsPopularProduct)
        {
            var popularProducts = await _productRepository.CountAsync(x => x.IsPopular);

            if (popularProducts >= 8)
            {
                throw new BadRequestException("En fazla 8 adet popüler ürün ekleyebilirsiniz");
            }
        }

        await _productRepository.AddAsync(product);
    }
    
    public async Task UpdateAsync(ProductUpdateReq model)
    {
        var product = await _productRepository.GetAsync(x => x.Id == model.Id);
        
        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        product.Name = model.ProductName;
        product.Description = model.ProductDescription;
        
        await _productRepository.UpdateAsync(product);
    }

    public async Task<ProductImageUpdateReturnModel> UpdateImageAsync(Guid id, string imageUrl)
    {
        var product = await _productRepository.GetAsync(x => x.Id == id);
        
        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }
        
        var oldImageName = product.ImageUrl;
        product.ImageUrl = imageUrl;
        
        await _productRepository.UpdateAsync(product);
        
        return new ProductImageUpdateReturnModel
        {
            OldImageName = oldImageName
        };
    }

    public async Task<ProductDeleteReturnModel> DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetAsync(x => x.Id == id);

        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }
        
        await _productRepository.DeleteAsync(product);
        
        return new ProductDeleteReturnModel
        {
            ProductImage = product.ImageUrl
        };
    }
}