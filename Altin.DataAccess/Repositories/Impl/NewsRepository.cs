using Altin.Core.Entities;

namespace Altin.DataAccess.Repositories.Impl;

public class NewsRepository : BaseRepository<News>, INewsRepository
{
    public NewsRepository(DatabaseContext context) : base(context)
    {
    }
}