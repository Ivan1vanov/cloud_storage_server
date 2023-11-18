using System.ComponentModel.DataAnnotations;

namespace CloudStorage.DTOs
{
    public class UploadDocumentRequestDto
    {
        public string? Description { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}