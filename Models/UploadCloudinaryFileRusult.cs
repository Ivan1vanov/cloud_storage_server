using System.Net;

namespace CloudStorage.Models
{
    public class UploadCloudinaryFileRusult {
        public Guid DokumentId { get; set; }
        public string DokumentName { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}