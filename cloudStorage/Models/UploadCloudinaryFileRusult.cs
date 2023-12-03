using System.Net;

namespace CloudStorage.Models
{
    public class UploadCloudinaryFileResult
    {
        public Guid DocumentId { get; set; }
        public string DocumentName { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string DocumentExtension { get; set; }
    }
}