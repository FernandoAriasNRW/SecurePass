using Microsoft.AspNetCore.Mvc;
using SecurePass.Auth.Domain;
using SecurePass.Auth.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecurePass.Auth.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class AuthController(IAuthService authService) : ControllerBase
  {
    private readonly IAuthService _authService = authService;

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
      var results = await _authService.Login(login);

      if (results == null)
      {
        return NotFound("Wrong Email or Password");
      }

      return Ok(results);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto register)
    {
      var user = await _authService.Register(register);

      if (user == 1)
      {
        return Ok();
      }

      return BadRequest("Something Wrong");
    }
  }
}