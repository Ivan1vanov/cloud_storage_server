using System.Threading.Tasks;
using CloudStorage.Contorllers;
using CloudStorage.DTOs;
using CloudStorage.Interfaces;
using CloudStorage.Models;
using CloudStorage.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CloudStorage.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private Mock<IAuthService> _authServiceMock = new Mock<IAuthService>();

        [Fact]
        public async Task SignUp_ValidRequest_ReturnsOk()
        {
            // Arrange
            _authServiceMock
                .Setup(services => services.SignUp(It.IsAny<UserSighUpRequestDto>()))
                .ReturnsAsync(AuthResultMocks.AuthSuccessResultMock);

            var controller = new AuthenticationController(_authServiceMock.Object);

            // Act
            var result = await controller.SignUp(AuthDtoMocks.UserSignUpRequestDtoMock);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var authResult = Assert.IsType<AuthResult>(okResult.Value);

            Assert.True(authResult.Success);
            Assert.Equal(authResult.Token, AuthResultMocks.AuthSuccessResultMock.Token);

        }

        [Fact]
        public async Task SignUp_ValidRequest_ReturnsUnauthorizedResultWithErrors()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock
                .Setup(services => services.SignUp(It.IsAny<UserSighUpRequestDto>()))
                .ReturnsAsync(AuthResultMocks.AuthUnsuccessResultMock);

            var controller = new AuthenticationController(authServiceMock.Object);

            // Act
            var result = await controller.SignUp(AuthDtoMocks.UserSignUpRequestDtoMock);

            // Assert
            var badRequestResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var authResult = Assert.IsType<AuthResult>(badRequestResult.Value);

            Assert.False(authResult.Success);
            Assert.Null(authResult.Token);

        }
        
        [Fact]
        public async Task SignIn_ValidRequest_ReturnsOk()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock
                .Setup(services => services.SignIn(AuthDtoMocks.UserSignInRequestDtoMock))
                .ReturnsAsync(AuthResultMocks.AuthSuccessResultMock);

            var controller = new AuthenticationController(authServiceMock.Object);

            // Act
            var result = await controller.SignIn(AuthDtoMocks.UserSignInRequestDtoMock);

            // Assert
            var badRequestResult = Assert.IsType<OkObjectResult>(result);
            var authResult = Assert.IsType<AuthResult>(badRequestResult.Value);

            Assert.True(authResult.Success);
            Assert.Equal(authResult.Token, AuthResultMocks.AuthSuccessResultMock.Token);

        }

        [Fact]
        public async Task SignIn_ValidRequest_ReturnsUnauthorizedResultWithErrors()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock
                .Setup(services => services.SignIn(AuthDtoMocks.UserSignInRequestDtoMock))
                .ReturnsAsync(AuthResultMocks.AuthUnsuccessResultMock);

            var controller = new AuthenticationController(authServiceMock.Object);

            // Act
            var result = await controller.SignIn(AuthDtoMocks.UserSignInRequestDtoMock);

            // Assert
            var badRequestResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var authResult = Assert.IsType<AuthResult>(badRequestResult.Value);

            Assert.False(authResult.Success);
            Assert.Null(authResult.Token);

        }
    }
}