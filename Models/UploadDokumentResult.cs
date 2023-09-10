namespace CloudStorage.Models
{
    public class UploadDokumentResult {
        public bool Success { get; set; }

        public List<string> Errors { get; set; }
        public Guid DokumentId { get; set; }

        public string DokumentName { get; set; }

        public string DokumentExtention { get; set; }
    }
}