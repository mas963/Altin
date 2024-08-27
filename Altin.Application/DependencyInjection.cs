using Altin.Application.Common.Email;
using Altin.Application.MappingProfiles;
using Altin.Application.Models.Product;
using Altin.Application.Models.Validators.Product;
using Altin.Application.Services;
using Altin.Application.Services.Impl;
using Altin.Shared;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Altin.Application;

public static class DependencyInjection
{
    public static IServiceCollection ApplicationDependencyInjection(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ITodoListService, TodoListService>();
        services.AddScoped<ITodoItemService, TodoItemService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<INewsService, NewsService>();
        
        // FluentValidation
        services.AddScoped<IValidator<ProductUpdateReq>, ProductUpdateReqValidator>();

        
        services.AddAutoMapper(typeof(IMappingProfilesMarker));

        services.AddSingleton(configuration.GetSection("SmtpSettings").Get<SmtpSettings>());

        return services;
    }
}