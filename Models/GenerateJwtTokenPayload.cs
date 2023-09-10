namespace CloudStorage.Models
{
    public class GenerateJwtTokenPayload {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}