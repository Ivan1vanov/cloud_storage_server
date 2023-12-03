using System.Collections.Generic;
using System.Threading.Tasks;
using CloudStorage.Constants.ErrorMessages;
using CloudStorage.Entity;
using CloudStorage.Interfaces;
using CloudStorage.Models;
using CloudStorage.Services;
using CloudStorage.Tests.Mocks;
using Moq;
using Xunit;

namespace CloudStorage.Tests.Services
{
    public class AuthServiceTests
    {

        private Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private Mock<IJwtTokenService> _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        private Mock<IBCryptHelpers> _bcryptHelpersMock = new Mock<IBCryptHelpers>();


        [Fact]
        public async Task SignIn_UserExistsAndPasswordMatches_ReturnsSuccessResult()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetByEmailAddress(It.IsAny<string>()))
                .ReturnsAsync(EntityMocks.User);

            _bcryptHelpersMock.Setup(bcrypt => bcrypt.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _jwtTokenServiceMock.Setup(jwt => jwt.GenerateAccessToken(It.IsAny<GenerateJwtTokenPayload>()))
                .Returns(AuthResultMocks.AuthSuccessResultMock.Token);

            var authService = new AuthService
            (
                _userRepositoryMock.Object,
                _jwtTokenServiceMock.Object,
                _bcryptHelpersMock.Object
            );

            // Act
            var result = await authService.SignIn(AuthDtoMocks.UserSignInRequestDtoMock);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(AuthResultMocks.AuthSuccessResultMock.Token, result.Token);
        }

        [Fact]
        public async Task SignIn_UserDoesNotExist_ReturnsUnsuccessResult()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetByEmailAddress(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var authService = new AuthService
            (
                _userRepositoryMock.Object,
                _jwtTokenServiceMock.Object,
                _bcryptHelpersMock.Object
            );

            // Act
            var result = await authService.SignIn(AuthDtoMocks.UserSignInRequestDtoMock);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(new List<string>() {
                AuthServiceErrorMessages.NoUserFoundForProvidedEmail
            }, result.Errors);
        }

        [Fact]
        public async Task SignIn_PasswordsDoesNotMatch_ReturnsUnsuccessResult()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetByEmailAddress(It.IsAny<string>()))
                .ReturnsAsync(EntityMocks.User);

            _bcryptHelpersMock.Setup(bcrypt => bcrypt.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            var authService = new AuthService
            (
                _userRepositoryMock.Object,
                _jwtTokenServiceMock.Object,
                _bcryptHelpersMock.Object
            );

            // Act
            var result = await authService.SignIn(AuthDtoMocks.UserSignInRequestDtoMock);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(new List<string>() {
                AuthServiceErrorMessages.InvalidCredentials
            }, result.Errors);
        }

        [Fact]
        public async Task SignUp_CreateNewUser_ReturnsSuccessResult()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.DoesUserExistByEmail(It.IsAny<string>()))
                .ReturnsAsync(false);

            _bcryptHelpersMock.Setup(bcrypt => bcrypt.HashPassword(It.IsAny<string>()))
                .Returns(EntityMocks.User.Password);

            _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<User>()))
                .ReturnsAsync(EntityMocks.User);

            _jwtTokenServiceMock.Setup(jwt => jwt.GenerateAccessToken(It.IsAny<GenerateJwtTokenPayload>()))
                .Returns(AuthResultMocks.AuthSuccessResultMock.Token);

            var authService = new AuthService
            (
                _userRepositoryMock.Object,
                _jwtTokenServiceMock.Object,
                _bcryptHelpersMock.Object
            );

            // Act
            var result = await authService.SignUp(AuthDtoMocks.UserSignUpRequestDtoMock);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(AuthResultMocks.AuthSuccessResultMock.Token, result.Token);
        }

        [Fact]
        public async Task SignUp_UserWithProvidedEmailExists_ReturnsUnsuccessResult()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.DoesUserExistByEmail(It.IsAny<string>()))
                .ReturnsAsync(true);

            var authService = new AuthService
            (
                _userRepositoryMock.Object,
                _jwtTokenServiceMock.Object,
                _bcryptHelpersMock.Object
            );

            // Act
            var result = await authService.SignUp(AuthDtoMocks.UserSignUpRequestDtoMock);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(new List<string>(){
                AuthServiceErrorMessages.UserWithProvidedEmailAlreadyExists
            }, result.Errors);
        }
    }
}
