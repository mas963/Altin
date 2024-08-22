using Altin.Application.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Altin.Application.Services;

public interface IUserService
{
    Task<Guid> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel);

    Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);

    Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel);

    Task<SignInResult> LoginAsync(LoginUserModel loginUserModel);

    Task LogoutAsync();
}