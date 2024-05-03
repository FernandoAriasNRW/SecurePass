using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurePass.Vaults.Domain;
using SecurePass.Vaults.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecurePass.Vaults.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VaultController(IVaultService vaultService) : ControllerBase
  {
    private readonly IVaultService _vaultService = vaultService;

    // GET: api/<VaultController>
    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<Vault>> Get()
    {
      if (HttpContext.User.Identity.IsAuthenticated)
      {
        if (HttpContext.User.Identity is ClaimsIdentity user)
        {
          string userId = user.Claims.First().Value;

          return await _vaultService.GetAll(userId);
        }
      }

      return await _vaultService.GetAll();
    }

    // GET api/<VaultController>/5
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
      var response = await _vaultService.GetById(id);

      if (response == null)
      {
        return NotFound($"Not found any record with this Id {id}");
      }

      return Ok(response);
    }

    // POST api/<VaultController>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Vault entity)
    {
      var response = await _vaultService.Create(entity);

      if (response == 1)
      {
        return Created();
      }

      return Problem("Something was wrong", null, 400);
    }

    // PUT api/<VaultController>/5
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] Vault entity)
    {
      var response = await _vaultService.Update(id, entity);

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

    // DELETE api/<VaultController>/5
    [Authorize]
    [HttpDelete("soft/{id}")]
    public async Task<int> SoftDelete(Guid id)
    {
      return await _vaultService.SoftDelete(id);
    }

    [Authorize("admin")]
    [HttpDelete("hard/{id}")]
    public async Task<int> HardDelete(Guid id)
    {
      return await _vaultService.HardDelete(id);
    }
  }
}