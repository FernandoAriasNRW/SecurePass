using System.ComponentModel.DataAnnotations;
using SecurePass.Auth.User.Domain;
using SecurePass.Repository;
using SecurePass.Vaults.Domain;

namespace SecurePass.Folders.Domain
{
  public class Folder : BaseEntity
  {
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public Guid VaultId { get; set; }

    public Vault? Vault { get; set; }
  }
}