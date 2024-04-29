using SecurePass.Registers.Domain;
using SecurePass.Vaults.Domain;

namespace SecurePass.Auth.User.Domain
{
  public class UpdateUser : UserEntity
  {
    public new string? Name { get; set; }
    public new string? LastName { get; set; }
    public new string? Email { get; set; }
    public new string? Password { get; set; }
    public new string? Role { get; set; }
    public new ICollection<VaultEntity>? Vaults { get; set; }
    public new ICollection<RecordEntity>? Registers { get; set; }
  }
}