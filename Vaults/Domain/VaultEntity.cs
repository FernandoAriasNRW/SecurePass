using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SecurePass.Auth.User.Domain;
using SecurePass.Registers.Domain;

namespace SecurePass.Vaults.Domain
{
  public class VaultEntity
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<RegisterEntity>? Registers { get; set; }

    [Required]
    public UserEntity User { get; set; }

    // Fecha de creación
    public DateTime CreatedAt { get; set; }

    // Fecha de última modificación
    public DateTime? UpdatedAt { get; set; }

    // Fecha de eliminación
    public DateTime? DeletedAt { get; set; }
  }
}