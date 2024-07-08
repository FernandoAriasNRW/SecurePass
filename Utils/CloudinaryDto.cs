using CloudinaryDotNet.Actions;

namespace SecurePass.Utils
{
  public class CloudinaryDto
  {
    public string PublicId { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public string Format { get; set; }

    public string CreatedAt { get; set; }

    public string Url { get; set; }

    public string SecureUrl { get; set; }

    public string Signature { get; set; }

    public Eager[] Eager { get; set; }
  }
}