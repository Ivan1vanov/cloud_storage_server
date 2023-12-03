using System.Text.Json;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CloudStorage.Config;
using CloudStorage.Interfaces;
using CloudStorage.Models;
using Microsoft.Extensions.Options;

namespace CloudStorage.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        Cloudinary _cloudinary;
        public CloudinaryService(IOptions<CloudinaryConfig> cloudinaryConfig)
        {
            Account accound = new Account(
            cloudinaryConfig.Value.CloudName,
            cloudinaryConfig.Value.ApiKey,
            cloudinaryConfig.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(accound);
        }

        public async Task<UploadCloudinaryFileResult> UploadFile(IFormFile file)
        {
            Guid documentId = Guid.NewGuid();

            string documentName = Path.GetFileNameWithoutExtension(file.FileName);
            string documentExtension = Path.GetExtension(file.FileName).Split(".").Last();

            using (var stream = file.OpenReadStream())
            {
                try
                {
                    var uploadParams = new RawUploadParams()
                    {
                        File = new FileDescription(documentName, stream),
                        PublicId = documentId.ToString(),
                    };
                    RawUploadResult uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    return new UploadCloudinaryFileResult()
                    {
                        StatusCode = uploadResult.StatusCode,
                        DocumentId = documentId,
                        DocumentName = documentName,
                        DocumentExtension = documentExtension,
                    };
                }
                catch (Exception error)
                {
                    throw error;
                }
            }
        }

    }
}