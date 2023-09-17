using CloudStorage.Constants;
using CloudStorage.DTOs;
using CloudStorage.Entity;
using CloudStorage.Interfaces;
using CloudStorage.Models;

namespace CloudStorage.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IBCryptHelpers _bcryptHelpers;

        public AuthService(
            IUserRepository repository,
            IJwtTokenService jwtTokenService,
            IBCryptHelpers BCryptHelpers
            )
        {
            _userRepository = repository;
            _jwtTokenService = jwtTokenService;
            _bcryptHelpers = BCryptHelpers;
        }


        public async Task<AuthResult> SignUp(UserSighUpRequestDto request)
        {
            bool doesUserExist = await _userRepository.DoesUserExistByEmail(request.Email);

            if (doesUserExist)
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>(){
                    AuthServiceErrorMessages.UserWithProvidedEmailAlreadyExists
                }
                };
            }

            string hashPassword = _bcryptHelpers.HashPassword(request.Password);

            var newUser = await _userRepository.CreateUser(new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = hashPassword,
            });

            var jwtToken = _jwtTokenService.GenerateAccessToken(new GenerateJwtTokenPayload()
            {
                UserId = newUser.Id,
                Email = newUser.Email
            });

            return new AuthResult()
            {
                Success = true,
                Token = jwtToken,
            };
        }

        public async Task<AuthResult> SignIn(UserSignInRequestDto data)
        {
            var user = await _userRepository.GetByEmailAddress(data.Email);

            if (user == null)
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>(){
                    AuthServiceErrorMessages.NoUserFoundForProvidedEmail
                },
                };
            }

            bool passwordVerified = _bcryptHelpers.VerifyPassword(data.Password, user.Password);

            if (!passwordVerified)
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>() {
                    AuthServiceErrorMessages.InvalidCredentials
                }
                };
            }

            var jwtToken = _jwtTokenService.GenerateAccessToken(new GenerateJwtTokenPayload()
            {
                Email = user.Email,
                UserId = user.Id,
            });

            return new AuthResult()
            {
                Success = true,
                Token = jwtToken,
            };
        }
    }
}
