using System.ComponentModel.DataAnnotations;
using SecurePass.Auth.User.Domain;
using SecurePass.Repository;

namespace SecurePass.Vaults.Domain
{
  public class Vault : BaseEntity
  {
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }
  }
}