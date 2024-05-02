using SecurePass.Vaults.Domain;

namespace SecurePass.Vaults.Services
{
  public interface IVaultService
  {
    public Task<List<Vault>> GetAll(string userId);

    public Task<List<Vault>> GetAll();

    public Task<Vault?> GetById(Guid id);

    public Task<int> Create(Vault entity);

    public Task<int> Update(Guid id, Vault entity);

    public Task<int> SoftDelete(Guid id);

    public Task<int> HardDelete(Guid id);

    public Task<List<Vault>> Search(string term);
  }
}