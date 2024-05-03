using Microsoft.EntityFrameworkCore;

namespace SecurePass.Repository
{
  public class Repository<TEntity>(
    ApiDbContext context
      ) : IRepository<TEntity> where TEntity : BaseEntity
  {
    private readonly ApiDbContext _context = context;

    protected DbSet<TEntity> Entities => _context.Set<TEntity>();

    public virtual async Task<int> Add(TEntity entity)
    {
      if (object.Equals(entity, null))
      {
        throw new ArgumentNullException(nameof(entity), $"{nameof(Add)} {nameof(entity)} must not be null");
      }

      await Entities.AddAsync(entity);

      return await _context.SaveChangesAsync();
    }

    public virtual async Task<int> SoftDelete(Guid id)
    {
      TEntity? entity = await GetById().FindAsync(id);

      if (object.Equals(entity, null))
      {
        return 0;
      }

      var newEntity = entity;

      newEntity.IsDeleted = true;
      newEntity.DeletedAt = DateTime.UtcNow;

      await Update(entity, newEntity);
      return await _context.SaveChangesAsync();
    }

    public virtual async Task<int> HardDelete(Guid id)
    {
      var entity = await GetById().FindAsync(id);

      if (object.Equals(entity, null))
      {
        return 0;
      }

      _context.Remove<TEntity>(entity);
      return await _context.SaveChangesAsync();
    }

    public virtual DbSet<TEntity> GetAll()
    {
      return Entities;
    }

    public virtual DbSet<TEntity> GetById()
    {
      return Entities;
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

    public virtual async Task<int> Update(TEntity entity, TEntity newEntity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException(nameof(entity), $"{nameof(Add)} {nameof(entity)} must not be null");
      }

      Entities.Entry(entity).CurrentValues.SetValues(newEntity);

      return await _context.SaveChangesAsync();
    }
  }
}