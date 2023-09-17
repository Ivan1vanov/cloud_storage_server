using CloudinaryDotNet.Actions;
using CloudStorage.Models;

namespace CloudStorage.Interfaces
{
    public interface ICloudinaryService
    {
        public Task<UploadCloudinaryFileRusult> UploadFile(IFormFile file);
    }
}
