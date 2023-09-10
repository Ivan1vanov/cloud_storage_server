using System.ComponentModel.DataAnnotations;

namespace CloudStorage.DTOs
{
    public class UserSignInRequestDto {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}