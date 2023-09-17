using System.IdentityModel.Tokens.Jwt;

namespace CloudStorage.Models
{
    public class TokenData
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}