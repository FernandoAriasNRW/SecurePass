using System.ComponentModel.DataAnnotations;
using SecurePass.Auth.User.Domain;
using SecurePass.Folders.Domain;
using SecurePass.Repository;
using SecurePass.Vaults.Domain;

namespace SecurePass.Registers.Domain
{
  public class Record : BaseEntity
  {
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public string? Url { get; set; }

    public Guid? VaultId { get; set; }

    public Vault? Vault { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public Guid? FolderId { get; set; }

    public Folder? Folder { get; set; }

    public string? ProfileUrl { get; set; }

    public string? ProfileId { get; set; }

    public Record()
    {
      Description = "";
    }
  }
}