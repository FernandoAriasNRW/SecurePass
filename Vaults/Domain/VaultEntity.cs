using System.ComponentModel.DataAnnotations;
using SecurePass.Auth.User.Domain;
using SecurePass.Registers.Domain;
using SecurePass.Repository;

namespace SecurePass.Vaults.Domain
{
  public class VaultEntity : BaseEntity
  {
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<RecordEntity>? Registers { get; set; }

    [Required]
    public UserEntity User { get; set; }
  }
}