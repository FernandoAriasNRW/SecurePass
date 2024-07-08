using CloudinaryDotNet.Actions;

namespace SecurePass.Utils
{
  public interface IUploadService
  {
    Task<ImageUploadResult> AddImageAsync(IFormFile file);

    Task<DeletionResult> DeleteImageAsync(string publicId);
  }
}