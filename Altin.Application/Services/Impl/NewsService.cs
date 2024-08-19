using Altin.Application.Exceptions;
using Altin.Application.Models.News;
using Altin.Application.Models.Product;
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

    public async Task<GetNewsModel> GetAsync(Guid id)
    {
        var news = await _newsRepository.GetAsync(x => x.Id == id);

        if (news == null)
        {
            throw new NotFoundException("News not found");
        }

        return new GetNewsModel
        {
            Id = news.Id,
            Title = news.Title,
            Content = news.Content,
            ImageName = news.ImageName
        };
    }
    
    public async Task<GetNewsModel> GetBySlugAsync(string slug)
    {
        var news = await _newsRepository.GetAsync(x => x.NormalizedTitle == slug);

        if (news == null)
        {
            throw new NotFoundException("News not found");
        }

        return new GetNewsModel
        {
            Id = news.Id,
            Title = news.Title,
            Content = news.Content,
            ImageName = news.ImageName,
            CreatedOn = news.CreatedOn
        };
    }

    public async Task<List<GetNewsModel>> GetListAsync()
    {
        var news = await _newsRepository.GetAllAsync(orderBy: x=> x.CreatedOn, descending: true);

        return news.Select(x => new GetNewsModel
        {
            Id = x.Id,
            Title = x.Title,
            Slug = x.NormalizedTitle,
            Content = x.Content,
            ImageName = x.ImageName,
            CreatedOn = x.CreatedOn
        }).ToList();
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

    public async Task EditAsync(EditNewsModel model)
    {
        var news = await _newsRepository.GetAsync(x => x.Id == model.Id);

        news.Title = model.Title;
        news.Content = model.Content;

        await _newsRepository.UpdateAsync(news);
    }

    public async Task<EditImageNewsReturnModel> EditImageAsync(EditImageNewsModel model)
    {
        var news = await _newsRepository.GetAsync(x => x.Id == model.Id);

        if (news == null)
        {
            throw new NotFoundException("News not found");
        }

        var oldImageName = news.ImageName;
        news.ImageName = model.Image;

        await _newsRepository.UpdateAsync(news);

        return new EditImageNewsReturnModel
        {
            OldImageName = oldImageName
        };
    }
}