using Microsoft.EntityFrameworkCore;
using SecurePass.Auth.User.Domain;
using SecurePass.Registers.Domain;
using SecurePass.Vaults.Domain;

namespace SecurePass.Repository
{
  public class ApiDbContext : DbContext
  {
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    { }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      // Obtener todas las entidades que están siendo añadidas, modificadas o eliminadas
      var entities = ChangeTracker.Entries()
          .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
          .ToList();

      try
      {
        // Configurar las fechas de creación, modificación y eliminación
        foreach (var entity in entities)
        {
          if (entity.State == EntityState.Added)
          {
            ((BaseEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
          }

          if (entity.State == EntityState.Modified || entity.State == EntityState.Deleted)
          {
            ((BaseEntity)entity.Entity).UpdatedAt = DateTime.UtcNow;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception($"Error: {ex}");
      }

      return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Configurar un filtro global para excluír las entidades eliminadas lógicamente
      modelBuilder.Entity<UserEntity>().HasQueryFilter(p => !p.DeletedAt.HasValue);
      modelBuilder.Entity<VaultEntity>().HasQueryFilter(p => !p.DeletedAt.HasValue);
      modelBuilder.Entity<RecordEntity>().HasQueryFilter(p => !p.DeletedAt.HasValue);
    }
  }
}