namespace SecurePass.Auth.Domain
{
  public class LoginResultDto
  {
    public string? Token { get; set; }

    public Guid UserId { get; set; }

    public string Role { get; set; }

    public DateTime ExpiresIn { get; set; }
  }
}