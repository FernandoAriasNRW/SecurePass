using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SecurePass.Auth.Domain;
using SecurePass.Auth.User.Services;
using SecurePass.Utils;

namespace SecurePass.Auth.Services
{
  public class AuthService(IUserService userService, IEncrypt encrypt, IConfiguration config) : IAuthService
  {
    private readonly IUserService _userService = userService;
    private readonly IEncrypt _encrypt = encrypt;
    private readonly IConfiguration _config = config;

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

      if (user == null)
      {
        return null;
      }

      if (this._encrypt.GetSHA256(data.Password) != user.Password)
      {
        return null;
      }

      var tokenHandler = new JwtSecurityTokenHandler();

      var byteKey = Encoding.UTF8.GetBytes(_config["jwtSecret"]);

      var description = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new("Id", user.Id.ToString()),
          new(ClaimTypes.Role, user.Role)
        }),
        Expires = DateTime.UtcNow.AddMinutes(15),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(description);

      return new LoginResultDto() { Token = tokenHandler.WriteToken(token), UserId = user.Id, Role = user.Role };
    }
  }
}