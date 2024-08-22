namespace Altin.Application.Models.User;

public class LoginUserModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; } = true;
}

public class LoginResponseModel
{
    public string Username { get; set; }

    public string Email { get; set; }
}