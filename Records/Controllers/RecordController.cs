using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SecurePass.Records.Services;
using SecurePass.Registers.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecurePass.Registers.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RecordController(IRecordService recordService) : ControllerBase
  {
    private readonly IRecordService _recordService = recordService;

    // GET: api/<RegisterController>
    [HttpGet]
    public async Task<IEnumerable<Record>> Get()
    {
      if (HttpContext.User.Identity is ClaimsIdentity user)
      {
        string userId = user.Claims.First().Value;

        return await _recordService.GetAll(userId);
      }

      return await _recordService.GetAll();
    }

    // GET api/<RegisterController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
      var response = await _recordService.GetById(id);

      if (response == null)
      {
        return NotFound($"Not found any record with this Id {id}");
      }

      return Ok(response);
    }

    // POST api/<RegisterController>
    [HttpPost]
    public async Task<int> Post([FromBody] Record entity)
    {
      return await _recordService.Create(entity);
    }

    // PUT api/<RegisterController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] Record entity)
    {
      var response = await _recordService.Update(id, entity);

      if (response == 2)
      {
        return NotFound($"Not found any recored to update with this Id {id}");
      }

      if (response == 0)
      {
        return Problem("Something was wrong");
      }

      return Ok(response);
    }

    // DELETE api/<RegisterController>/5
    [HttpDelete("soft/{id}")]
    public Task<int> SoftDelete(Guid id)
    {
      return _recordService.SoftDelete(id);
    }

    [HttpDelete("hard/{id}")]
    public Task<int> HardDelete(Guid id)
    {
      return _recordService.HardDelete(id);
    }
  }
}