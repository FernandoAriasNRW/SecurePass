namespace SecurePass.Repository
{
  public interface IRepository<TEntity>
  {
    public Task<TEntity?> GetById(Guid id);

    public Task<List<TEntity>> GetAll();

    public Task<int> Add(TEntity entity);

    public Task<int> Update(TEntity entity);

    public Task<int> SoftDelete(Guid id);

    public Task<int> HardDelete(Guid id);

    //public Task<List<TEntity>> Search(string keyword);
  }
}