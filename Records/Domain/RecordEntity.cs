using System.ComponentModel.DataAnnotations;
using SecurePass.Auth.User.Domain;
using SecurePass.Repository;
using SecurePass.Vaults.Domain;

namespace SecurePass.Registers.Domain
{
  public class RecordEntity : BaseEntity
  {
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

    public RecordEntity()
    {
      Description = "";
    }
  }
}