using SecurePass.Folders.Domain;

namespace SecurePass.Folders.Services
{
  public interface IFolderService
  {
    public Task<List<Folder>> GetAll(string userId);

    public Task<List<Folder>> GetAll();

    public Task<Folder?> GetById(Guid id);

    public Task<int> Create(Folder entity);

    public Task<int> Update(Guid id, Folder entity);

    public Task<int> SoftDelete(Guid id);

    public Task<int> HardDelete(Guid id);

    public Task<List<Folder>> Search(string term);
  }
}