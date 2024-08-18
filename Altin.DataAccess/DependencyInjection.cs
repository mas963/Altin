using Altin.DataAccess.Repositories;
using Altin.DataAccess.Repositories.Impl;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Altin.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection DataAccessDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddScoped<ITodoListRepository, TodoListRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<INewsRepository, NewsRepository>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(connectionString));
                
        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>();
        
        return services;
    }
}