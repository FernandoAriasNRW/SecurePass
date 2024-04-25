namespace SecurePass.Repository
{
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    private readonly ApiDbContext _context;

    public Repository(
      ApiDbContext context
      )
    {
      _context = context;
    }

    public virtual async Task<int> Add(TEntity entity)
    {
      if (object.Equals(entity, null))
      {
        throw new ArgumentNullException(nameof(entity), $"{nameof(Add)} {nameof(entity)} must not be null");
      }

      await _context.AddAsync<TEntity>(entity);
      return await _context.SaveChangesAsync();
    }

    public virtual async Task Delete(Guid id)
    {
      var entity = await _context.FindAsync<TEntity>(id);

      if (object.Equals(entity, null))
      {
        throw new BadHttpRequestException($"{nameof(TEntity)} was not found");
      }

      _context.Remove<TEntity>(entity);
      await _context.SaveChangesAsync();
    }

    public virtual IQueryable<TEntity> GetAll()
    {
      return _context.Set<TEntity>();
    }

    public virtual async Task<TEntity> GetById(Guid id)
    {
      var entity = await _context.FindAsync<TEntity>(id);

      return entity ?? throw new BadHttpRequestException($"Not Found Any {nameof(TEntity)} with the Id {id}");
    }

    //public virtual Task<List<TEntity>> Search(string keyword)
    //{
    //  if (!string.IsNullOrEmpty(keyword))
    //  {
    //    foreach (var item in keyword.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //    {
    //      _context.Where<TEntity>(entity => entity);
    //    }
    //  }
    //}

    public virtual async Task Update(Guid id, TEntity entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException(nameof(entity), $"{nameof(Add)} {nameof(entity)} must not be null");
      }

      var entityFound = await _context.FindAsync<TEntity>(id);

      if (object.Equals(entityFound, null))
      {
        throw new BadHttpRequestException($"{nameof(TEntity)} was not found");
      }

      _context.Update<TEntity>(entity);
      await _context.SaveChangesAsync();
    }
  }
}