using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurePass.Auth.User.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860 b5692500175fad6bb2b306aa20ff58423c79b130ef310fb3caa924e0f28bc61d
namespace SecurePass.Auth.User.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController(IUserService userService) : ControllerBase
  {
    private readonly IUserService _userService = userService;

    // GET: api/<UserController>
    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<IEnumerable<Domain.User>> GetAll()
    {
      return await _userService.GetAll();
    }

    // GET api/<UserController>/5
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
      var entity = await _userService.GetById(id);

      if (entity == null)
      {
        return NotFound();
      }

      return Ok(entity);
    }

    // POST api/<UserController>
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Domain.User user)
    {
      var response = await _userService.Create(user);

      if (response == 1)
      {
        return Created();
      }

      if (response == 2)
      {
        return BadRequest("An account with this email already exist");
      }

      return Problem("Something was wrong", null, 400);
    }

    // PUT api/<UserController>/5
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] Domain.User user)
    {
      var response = await _userService.Update(id, user);

      if (response == 1)
      {
        return Ok("User Succesfully Updated");
      }

      if (response == -1)
      {
        return NotFound($"Not Found any User with the Id {id}");
      }

      return Problem("Something was wrong", null, 400);
    }

    // DELETE api/<UserController>/5
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var response = await _userService.Delete(id);

      if (response == 1)
      {
        return Ok(response);
      }

      return NotFound("Not found User to delete");
    }
  }
}