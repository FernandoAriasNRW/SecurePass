using System.ComponentModel.DataAnnotations;
using SecurePass.Repository;

namespace SecurePass.Auth.User.Domain
{
  public class User : BaseEntity
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

    public User()
    {
      Role = "Client";
    }
  }
}