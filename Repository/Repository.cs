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
      TEntity? entity = await GetById(id);

      if (object.Equals(entity, null))
      {
        return 0;
      }

      entity.IsDeleted = true;
      entity.DeletedAt = DateTime.UtcNow;

      await Update(entity);
      return await _context.SaveChangesAsync();
    }

    public virtual async Task<int> HardDelete(Guid id)
    {
      var entity = await GetById(id);

      if (object.Equals(entity, null))
      {
        return 0;
      }

      _context.Remove<TEntity>(entity);
      return await _context.SaveChangesAsync();
    }

    public virtual async Task<List<TEntity>> GetAll()
    {
      return await Entities.ToListAsync();
    }

    public virtual async Task<TEntity?> GetById(Guid id)
    {
      return await Entities.FindAsync(id);
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

    public virtual async Task<int> Update(TEntity entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException(nameof(entity), $"{nameof(Add)} {nameof(entity)} must not be null");
      }

      Entities.Update(entity);

      return await _context.SaveChangesAsync();
    }
  }
}