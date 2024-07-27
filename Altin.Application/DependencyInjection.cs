using Altin.Application.Common.Email;
using Altin.Application.MappingProfiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Altin.Application;

public static class DependencyInjection
{
    public static IServiceCollection ApplicationDependencyInjection(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(IMappingProfilesMarker));
        
        services.AddSingleton(configuration.GetSection("SmtpSettings").Get<SmtpSettings>());

        return services;
    }
}