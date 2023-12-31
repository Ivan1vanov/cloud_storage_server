using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CloudStorage.Entity
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        public List<Document> CreatedDocuments { get; set; }
        public List<Document> AccessedDocuments { get; set; }
    }
}