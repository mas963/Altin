using Altin.Core.Entities;

namespace Altin.DataAccess.Repositories.Impl;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DatabaseContext context) : base(context)
    {
    }
}