using CloudStorage.DTOs;
using CloudStorage.Entity;
using CloudStorage.Interfaces;
using CloudStorage.Models;

namespace CloudStorage.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRespository _userRepository;

        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(
            IUserRespository repository,
            IJwtTokenService jwtTokenService
            )
        {
            _userRepository = repository;
            _jwtTokenService = jwtTokenService;
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
                    "User with provided email already exists"
                }
                };
            }

            string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

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
                    "User with provided email address does not exist"
                },
                };
            }

            bool passwordVerified = BCrypt.Net.BCrypt.Verify(data.Password, user.Password);

            if (!passwordVerified)
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>() {
                    "Invalid credentials"
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
