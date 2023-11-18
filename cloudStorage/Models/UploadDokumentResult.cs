namespace CloudStorage.Models
{
    public class UploadDocumentResult
    {
        public bool Success { get; set; }

        public List<string> Errors { get; set; }
        public Guid DocumentId { get; set; }

        public string DocumentName { get; set; }

        public string DocumentExtension { get; set; }
    }
}