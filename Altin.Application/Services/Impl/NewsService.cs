using Altin.Application.Models.News;
using Altin.Core.Entities;
using Altin.DataAccess.Repositories;

namespace Altin.Application.Services.Impl;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;

    public NewsService(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public async Task CreateAsync(CreateNewsModel model)
    {
        var news = new News
        {
            Title = model.Title,
            Content = model.Content,
            ImageName = model.Image
        };
        
        await _newsRepository.AddAsync(news);
    }
}