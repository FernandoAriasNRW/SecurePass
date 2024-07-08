using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace SecurePass.Utils
{
  public class UploadService : IUploadService
  {
    private readonly Cloudinary _cloudinary;

    public UploadService(IConfiguration config)
    {
      var account = new Account(config["cloudinary:cloudName"], config["cloudinary:apiKey"], config["cloudinary:apiSecret"]);
      _cloudinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> AddImageAsync(IFormFile file)
    {
      var uploadResult = new ImageUploadResult();

      if (file.Length > 0)
      {
        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams()
        {
          File = new FileDescription(file.FileName, stream),
          Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
        };

        uploadResult = await _cloudinary.UploadAsync(uploadParams);
      }
      return uploadResult;
    }

    public async Task<DeletionResult> DeleteImageAsync(string publicId)
    {
      var deleteParams = new DeletionParams(publicId);

      var result = await _cloudinary.DestroyAsync(deleteParams);

      return result;
    }
  }
}