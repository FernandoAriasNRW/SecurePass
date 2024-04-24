using Microsoft.AspNetCore.Mvc;
using SecurePass.Auth.Domain;
using SecurePass.Auth.User.Services;

namespace SecurePass.Auth.Services
{
  public class AuthService(IUserService userService)
  {
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<LoginResultDto> Login(LoginDto body)
    {
      var user = await _userService.GetByEmail(body.Email);

      return new LoginResultDto();
    }
  }
}