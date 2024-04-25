namespace SecurePass.Repository
{
  public interface IRepository<TEntity>
  {
    public Task<TEntity> GetById(Guid id);

    public IQueryable<TEntity> GetAll();

    public Task<int> Add(TEntity entity);

    public Task Update(Guid id, TEntity entity);

    public Task Delete(Guid id);

    //public Task<List<TEntity>> Search(string keyword);
  }
}