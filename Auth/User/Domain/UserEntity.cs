using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SecurePass.Registers.Domain;
using SecurePass.Vaults.Domain;

namespace SecurePass.Auth.User.Domain
{
  public class UserEntity
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; init; }

    [Required]
    public string Password { get; set; }

    public string Role { get; set; }

    public ICollection<VaultEntity>? Vaults { get; set; }

    public ICollection<RegisterEntity>? Registers { get; set; }

    public UserEntity()
    {
      Role = "Client";
    }

    // Fecha de creación
    public DateTime CreatedAt { get; set; }

    // Fecha de última modificación
    public DateTime? UpdatedAt { get; set; }

    // Fecha de eliminación
    public DateTime? DeletedAt { get; set; }
  }
}