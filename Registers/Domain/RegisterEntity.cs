using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SecurePass.Auth.User.Domain;
using SecurePass.Vaults.Domain;

namespace SecurePass.Registers.Domain
{
  public class RegisterEntity
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public string? Url { get; set; }

    public VaultEntity? Vault { get; set; }

    [Required]
    public UserEntity User { get; set; }

    public RegisterEntity()
    {
      Description = "";
    }

    // Fecha de creación
    public DateTime CreatedAt { get; set; }

    // Fecha de última modificación
    public DateTime? UpdatedAt { get; set; }

    // Fecha de eliminación
    public DateTime? DeletedAt { get; set; }
  }
}