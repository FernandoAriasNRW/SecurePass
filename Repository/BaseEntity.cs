using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurePass.Repository
{
  public class BaseEntity
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public bool IsDeleted { get; set; }

    // Fecha de creación
    public DateTime CreatedAt { get; set; }

    // Fecha de última modificación
    public DateTime? UpdatedAt { get; set; }

    // Fecha de eliminación
    public DateTime? DeletedAt { get; set; }
  }
}