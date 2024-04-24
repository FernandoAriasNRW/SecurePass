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
    public async Task<UserEntity> Get(Guid id)
    {
      return await _userService.GetById(id);
    }

    // POST api/<UserController>
    [HttpPost]
    public async Task<OkResult> Post([FromBody] UserEntity value)
    {
      await _userService.Create(value);

      return Ok();
    }

    // PUT api/<UserController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UserController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}