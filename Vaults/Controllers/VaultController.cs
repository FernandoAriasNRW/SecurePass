using System.Security.Claims;
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
    [HttpGet]
    public async Task<IEnumerable<Vault>> Get()
    {
      if (HttpContext.User.Identity is ClaimsIdentity user)
      {
        string userId = user.Claims.First().Value;

        return await _vaultService.GetAll(userId);
      }

      return await _vaultService.GetAll();
    }

    // GET api/<VaultController>/5
    [HttpGet("{id}")]
    public async Task<Vault?> Get(Guid id)
    {
      return await _vaultService.GetById(id);
    }

    // POST api/<VaultController>
    [HttpPost]
    public void Post([FromBody] Vault value)
    {
    }

    // PUT api/<VaultController>/5
    [HttpPut("{id}")]
    public void Put(Guid id, [FromBody] string value)
    {
    }

    // DELETE api/<VaultController>/5
    [HttpDelete("soft/{id}")]
    public Task<int> SoftDelete(Guid id)
    {
      return _vaultService.SoftDelete(id);
    }

    [HttpDelete("hard/{id}")]
    public Task<int> HardDelete(Guid id)
    {
      return _vaultService.HardDelete(id);
    }
  }
}