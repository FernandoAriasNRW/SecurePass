using Microsoft.EntityFrameworkCore;
using SecurePass.Registers.Domain;
using SecurePass.Repository;

namespace SecurePass.Records.Services
{
  public class RecordService(IRepository<Record> recordRepository) : IRecordService
  {
    private readonly IRepository<Record> _recordRepository = recordRepository;

    public async Task<int> Create(Record entity)
    {
      return await _recordRepository.Add(entity);
    }

    public async Task<int> SoftDelete(Guid id)
    {
      return await _recordRepository.SoftDelete(id);
    }

    public async Task<int> HardDelete(Guid id)
    {
      return await _recordRepository.HardDelete(id);
    }

    public async Task<List<Record>> GetAll()
    {
      return await _recordRepository.GetAll().Include(r => r.User).Include(r => r.Vault).ToListAsync();
    }

    public async Task<List<Record>> GetAll(string userId)
    {
      return await _recordRepository.GetAll().Include(r => r.User).Include(r => r.Vault).Where(record => record.UserId.ToString() == userId).ToListAsync();
    }

    public async Task<Record?> GetById(Guid id)
    {
      return await _recordRepository.GetById().Where(record => record.Id == id).Include(r => r.User).Include(r => r.Vault).FirstOrDefaultAsync();
    }

    public async Task<List<Record>> Search(string term)
    {
      return await _recordRepository.GetAll().Include(r => r.User).ToListAsync();
    }

    public async Task<int> Update(Guid id, Record entity)
    {
      ArgumentNullException.ThrowIfNull(entity);

      var entityFound = await _recordRepository.GetById().FindAsync(id);

      if (entityFound == null)
      {
        return 2;
      }

      return await _recordRepository.Update(entityFound, entity);
    }
  }
}