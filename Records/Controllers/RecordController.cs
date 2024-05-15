using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<Record>> Get()
    {
      if (HttpContext.User.Identity.IsAuthenticated)
      {
        if (HttpContext.User.Identity is ClaimsIdentity user)
        {
          string userId = user.Claims.First().Value;

          return await _recordService.GetAll(userId);
        }
      }

      return await _recordService.GetAll();
    }

    // GET api/<RegisterController>/5
    [Authorize]
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
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Record entity)
    {
      var response = await _recordService.Create(entity);

      if (response == 1)
      {
        return Created();
      }

      return Problem("Something was wrong", null, 400);
    }

    // PUT api/<RegisterController>/5
    [Authorize]
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

    [Authorize]
    [HttpPut("{recordId}/vault/{vaultId}")]
    public async Task<IActionResult> AddRecord(Guid vaultId, Guid recordId, [FromBody] Record entity)
    {
      entity.VaultId = vaultId;

      var response = await _recordService.Update(recordId, entity);

      if (response == 2)
      {
        return NotFound($"Not found any recored to update with this Id {recordId}");
      }

      if (response == 0)
      {
        return Problem("Something was wrong");
      }

      return Ok("Record succesfull Adding to the vault");
    }

    // DELETE api/<RegisterController>/5
    [Authorize]
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