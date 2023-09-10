using CloudStorage.DTOs;
using CloudStorage.Models;

namespace CloudStorage.Interfaces
{
    public interface IAuthService {
        Task<AuthResult> SignUp(UserSighUpRequestDto request);

        Task<AuthResult> SignIn(UserSignInRequestDto request);

    }
}