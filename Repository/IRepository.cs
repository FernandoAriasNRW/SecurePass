using Microsoft.EntityFrameworkCore;

namespace SecurePass.Repository
{
  public interface IRepository<TEntity> where TEntity : class
  {
    public DbSet<TEntity> GetById();

    public DbSet<TEntity> GetAll();

    public Task<int> Add(TEntity entity);

    public Task<int> Update(TEntity entity);

    public Task<int> SoftDelete(Guid id);

    public Task<int> HardDelete(Guid id);

    //public Task<List<TEntity>> Search(string keyword);
  }
}