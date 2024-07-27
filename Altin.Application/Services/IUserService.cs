using Altin.Application.Models.User;

namespace Altin.Application.Services;

public interface IUserService
{
    Task<Guid> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel);

    Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);

    Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel);
    
    Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);
}