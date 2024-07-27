using Altin.Application.Models.User;
using Altin.DataAccess;
using AutoMapper;

namespace Altin.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, ApplicationUser>();
    }
}