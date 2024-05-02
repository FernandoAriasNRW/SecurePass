using Microsoft.EntityFrameworkCore;
using SecurePass.Repository;
using SecurePass.Vaults.Domain;

namespace SecurePass.Vaults.Services
{
  public class VaultService(IRepository<Vault> vaultRepository) : IVaultService
  {
    private readonly IRepository<Vault> _vaultRepository = vaultRepository;

    public async Task<int> Create(Vault entity)
    {
      return await _vaultRepository.Add(entity);
    }

    public async Task<List<Vault>> GetAll(string userId)
    {
      return await _vaultRepository.GetAll().Include(v => v.User).Where(vault => vault.UserId.ToString() == userId).ToListAsync();
    }

    public async Task<List<Vault>> GetAll()
    {
      return await _vaultRepository.GetAll().Include(v => v.User).ToListAsync();
    }

    public async Task<Vault?> GetById(Guid id)
    {
      return await _vaultRepository.GetById().Where(v => v.Id == id).Include(v => v.User).FirstOrDefaultAsync();
    }

    public async Task<int> HardDelete(Guid id)
    {
      return await _vaultRepository.HardDelete(id);
    }

    public async Task<List<Vault>> Search(string term)
    {
      return await _vaultRepository.GetAll().Where(vault => vault.Name.Contains(term)).Include(v => v.User).ToListAsync();
    }

    public async Task<int> SoftDelete(Guid id)
    {
      return await _vaultRepository.SoftDelete(id);
    }

    public async Task<int> Update(Guid id, Vault entity)
    {
      ArgumentNullException.ThrowIfNull(entity);

      var entityFound = await _vaultRepository.GetById().FindAsync(id);

      if (entityFound == null)
      {
        return 2;
      }

      return await _vaultRepository.Update(entity);
    }
  }
}