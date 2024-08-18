using Altin.Application.Models.News;

namespace Altin.Application.Services;

public interface INewsService
{
    Task CreateAsync(CreateNewsModel model);
}