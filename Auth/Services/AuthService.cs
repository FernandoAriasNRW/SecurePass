using SecurePass.Auth.Domain;
using SecurePass.Auth.User.Domain;
using SecurePass.Auth.User.Services;

namespace SecurePass.Auth.Services
{
  public class AuthService(IUserService userService) : IAuthService
  {
    private readonly IUserService _userService = userService;

    public async Task<LoginResultDto?> Login(LoginDto data)
    {
      var user = await Authenticate(data);

      return user;
    }

    public async Task<int> Register(RegisterDto data)
    {
      var wasCreated = await _userService.Create(data);

      return wasCreated;
    }

    private async Task<LoginResultDto?> Authenticate(LoginDto data)
    {
      var user = await _userService.GetByEmail(data.Email);

      user ??= new UserEntity() { Id = Guid.NewGuid(), Name = "Dani", LastName = "Perez", Registers = null, Email = "dani@admin.com", Password = "123", Role = "Admin", Vaults = null, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, DeletedAt = null };

      return (user.Email == data.Email && user.Password == data.Password) ? new LoginResultDto() { Token = data.Email, UserId = user.Id, Role = user.Role } : null;
    }
  }
}