using Microsoft.AspNetCore.Mvc;
using SecurePass.Auth.User.Domain;
using SecurePass.Auth.User.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecurePass.Auth.User.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController(IUserService userService) : ControllerBase
  {
    private readonly IUserService _userService = userService;

    // GET: api/<UserController>
    [HttpGet]
    public IEnumerable<UserEntity> GetAll()
    {
      return (IEnumerable<UserEntity>)_userService.GetAll();
    }

    // GET api/<UserController>/5
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
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserEntity user)
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
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UserEntity user)
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