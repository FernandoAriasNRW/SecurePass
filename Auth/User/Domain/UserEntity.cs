using System.ComponentModel.DataAnnotations;
using SecurePass.Registers.Domain;
using SecurePass.Repository;
using SecurePass.Vaults.Domain;

namespace SecurePass.Auth.User.Domain
{
  public class UserEntity : BaseEntity
  {
    [Required]
    public string Name { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public string Role { get; set; }

    public ICollection<VaultEntity>? Vaults { get; set; }

    public ICollection<RecordEntity>? Registers { get; set; }

    public UserEntity()
    {
      Role = "Client";
    }
  }
}