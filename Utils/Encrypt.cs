using System.Security.Cryptography;
using System.Text;

namespace SecurePass.Utils
{
  public class Encrypt : IEncrypt
  {
    protected Encrypt()
    { }

    public string GetSHA256(string str)
    {
      ASCIIEncoding encoding = new();

      StringBuilder sb = new();

      byte[] stream = SHA256.HashData(encoding.GetBytes(str));

      for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);

      return sb.ToString();
    }
  }
}