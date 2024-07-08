using Microsoft.EntityFrameworkCore;
using SecurePass.Folders.Domain;
using SecurePass.Repository;

namespace SecurePass.Folders.Services
{
  public class FolderService(IRepository<Folder> folderRepository) : IFolderService
  {
    private readonly IRepository<Folder> _folderRepository = folderRepository;

    public async Task<int> Create(Folder entity)
    {
      return await _folderRepository.Add(entity);
    }

    public async Task<List<Folder>> GetAll(string userId)
    {
      return await _folderRepository.GetAll().Include(f => f.User).Include(r => r.Vault).Where(folder => folder.UserId.ToString() == userId).ToListAsync();
    }

    public async Task<List<Folder>> GetAll()
    {
      return await _folderRepository.GetAll().Include(f => f.User).Include(r => r.Vault).ToListAsync();
    }

    public async Task<Folder?> GetById(Guid id)
    {
      return await _folderRepository.GetById().Where(f => f.Id == id).Include(f => f.User).FirstOrDefaultAsync();
    }

    public async Task<int> HardDelete(Guid id)
    {
      return await _folderRepository.HardDelete(id);
    }

    public async Task<List<Folder>> Search(string term)
    {
      return await _folderRepository.GetAll().Where(folder => folder.Name.Contains(term)).Include(f => f.User).Include(r => r.Vault).ToListAsync();
    }

    public async Task<int> SoftDelete(Guid id)
    {
      return await _folderRepository.SoftDelete(id);
    }

    public async Task<int> Update(Guid id, Folder entity)
    {
      ArgumentNullException.ThrowIfNull(entity);

      var entityFound = await _folderRepository.GetById().FindAsync(id);

      if (entityFound == null)
      {
        return 2;
      }

      return await _folderRepository.Update(entityFound, entity);
    }
  }
}