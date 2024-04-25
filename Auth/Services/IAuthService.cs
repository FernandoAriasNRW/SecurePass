using SecurePass.Auth.Domain;

namespace SecurePass.Auth.Services
{
  public interface IAuthService
  {
    public Task<LoginResultDto> Login(LoginDto data);

    public Task<int> Register(RegisterDto data);
  }
}