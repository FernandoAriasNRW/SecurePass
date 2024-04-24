using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecurePass.Auth.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    [HttpPost]
    public void Login([FromBody] string value)
    {
    }

    [HttpPost]
    public void Register([FromBody] string value)
    {
    }
  }
}