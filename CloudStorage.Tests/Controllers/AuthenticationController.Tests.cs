using System.Threading.Tasks;
using CloudStorage.Constants;
using CloudStorage.Contorllers;
using CloudStorage.DTOs;
using CloudStorage.Interfaces;
using CloudStorage.Models;
using CloudStorage.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CloudStorage.Tests.Controllers
{
    public class AuthenticationControllerTests
    {

        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<HttpContext> _httpContextMock;
        private readonly AuthenticationController _authController;

        public AuthenticationControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _httpContextMock = new Mock<HttpContext>();
            _authController = new AuthenticationController(_authServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _httpContextMock.Object
                }
            };
        }

        [Fact]
        public async Task SignUp_ValidRequest_ReturnsOk()
        {
            // Arrange
            _authServiceMock.Setup(services => services.SignUp(It.IsAny<UserSighUpRequestDto>()))
                .ReturnsAsync(AuthResultMocks.AuthSuccessResultMock);
            _httpContextMock.Setup(s => s.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>())).Callback(() => { });

            // Act
            var result = await _authController.SignUp(AuthDtoMocks.UserSignUpRequestDtoMock);

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
            _authServiceMock
                .Setup(services => services.SignUp(It.IsAny<UserSighUpRequestDto>()))
                .ReturnsAsync(AuthResultMocks.AuthUnsuccessResultMock);

            // Act
            var result = await _authController.SignUp(AuthDtoMocks.UserSignUpRequestDtoMock);

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
            _authServiceMock
                .Setup(s => s.SignIn(AuthDtoMocks.UserSignInRequestDtoMock))
                .ReturnsAsync(AuthResultMocks.AuthSuccessResultMock);
            _httpContextMock.Setup(s => s.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>())).Callback(() => { });

            // Act
            var result = await _authController.SignIn(AuthDtoMocks.UserSignInRequestDtoMock);

            // Assert
            _httpContextMock.Verify(c => c.Response.Cookies.Append(
                CookieKeyNames.access_token,
                AuthResultMocks.AuthSuccessResultMock.Token,
                It.IsAny<CookieOptions>()
            ), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var authResult = Assert.IsType<AuthResult>(okResult.Value);

            Assert.True(authResult.Success);
            Assert.Equal(authResult.Token, AuthResultMocks.AuthSuccessResultMock.Token);

        }

        [Fact]
        public async Task SignIn_ValidRequest_ReturnsUnauthorizedResultWithErrors()
        {
            // Arrange
            _authServiceMock
                .Setup(services => services.SignIn(AuthDtoMocks.UserSignInRequestDtoMock))
                .ReturnsAsync(AuthResultMocks.AuthUnsuccessResultMock);

            // Act
            var result = await _authController.SignIn(AuthDtoMocks.UserSignInRequestDtoMock);

            // Assert
            var badRequestResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var authResult = Assert.IsType<AuthResult>(badRequestResult.Value);

            Assert.False(authResult.Success);
            Assert.Null(authResult.Token);

        }
    }
}