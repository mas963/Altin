using Altin.Core.Entities;

namespace Altin.DataAccess.Repositories.Impl;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(DatabaseContext context) : base(context)
    {
    }
}