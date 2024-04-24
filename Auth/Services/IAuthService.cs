using SecurePass.Auth.Domain;

namespace SecurePass.Auth.Services
{
  public interface IAuthService
  {
    public LoginResultDto Login(LoginDto data);

    public void Register(RegisterDto data);
  }
}