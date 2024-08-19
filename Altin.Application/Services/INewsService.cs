using Altin.Application.Models.News;

namespace Altin.Application.Services;

public interface INewsService
{
    Task<List<GetNewsModel>> GetListAsync();
    Task<GetNewsModel> GetBySlugAsync(string slug);
    Task<GetNewsModel> GetAsync(Guid id);
    Task CreateAsync(CreateNewsModel model);
    Task EditAsync(EditNewsModel model);
    Task<EditImageNewsReturnModel> EditImageAsync(EditImageNewsModel model);
}