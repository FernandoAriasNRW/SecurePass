using SecurePass.Registers.Domain;

namespace SecurePass.Records.Services
{
  public interface IRecordService
  {
    public Task<List<Record>> GetAll(string userId);

    public Task<List<Record>> GetAll();

    public Task<Record?> GetById(Guid id);

    public Task<int> Create(Record entity);

    public Task<int> Update(Guid id, Record entity);

    public Task<int> SoftDelete(Guid id);

    public Task<int> HardDelete(Guid id);

    public Task<List<Record>> Search(string term);
  }
}