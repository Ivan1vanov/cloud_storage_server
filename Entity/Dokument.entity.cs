using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace CloudStorage.Entity
{
    public class Dokument
    {
        [Key]
        public Guid Id { get; set; }
        public string? Description { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public User Owner { get; set; }

        [Required]
        [ForeignKey("OwnerId")]
        public Guid OwnerId { get; set; }
        public List<User> AllowedUsers { get; set; }
    }
}