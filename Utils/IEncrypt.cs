namespace SecurePass.Utils
{
  public interface IEncrypt
  {
    public string GetSHA256(string str);
  }
}