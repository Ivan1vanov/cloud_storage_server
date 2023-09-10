using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CloudStorage.Config;
using CloudStorage.Interfaces;
using CloudStorage.Models;
using Microsoft.Extensions.Options;

namespace CloudStorage.Services
{
    public class CloudinaryService : ICloudinaryService {
        Cloudinary _cloudinary;
        public CloudinaryService(IOptions<CloudinaryConfig> cloudinaryConfig) {
            Account accound = new Account(
            cloudinaryConfig.Value.CloudName,
            cloudinaryConfig.Value.ApiKey,
            cloudinaryConfig.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(accound);
        }

        public async Task<UploadCloudinaryFileRusult> UploadFile(IFormFile file) {
            Guid dokumentId = Guid.NewGuid();
            string dokumentName = file.Name;

            using (var stream = file.OpenReadStream()){
            try
            {
                var uploadParams = new RawUploadParams()
                {
                File = new FileDescription(dokumentName, stream),
                PublicId=dokumentId.ToString(),
                };
                RawUploadResult  uploadResult = _cloudinary.Upload(uploadParams);

                return new UploadCloudinaryFileRusult(){
                    StatusCode = uploadResult.StatusCode,
                    DokumentId = dokumentId,
                    DokumentName = dokumentName,
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