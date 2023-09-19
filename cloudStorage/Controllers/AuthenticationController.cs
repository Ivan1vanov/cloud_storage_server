
using CloudStorage.Constants;
using CloudStorage.DTOs;
using CloudStorage.Helpers;
using CloudStorage.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorage.Contorllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signUp")]
        public async Task<ActionResult> SignUp([FromBody] UserSighUpRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.SignUp(request);

            if (!result.Success)
            {
                return Unauthorized(result);
            }

            Response.Cookies.Append(CookieKeyNames.access_token, result.Token, CookieHelpers.GetAuthCookieOptions());
            return Ok(result);
        }

        [HttpPost("signIn")]
        public async Task<ActionResult> SignIn([FromBody] UserSignInRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.SignIn(request);

            if (!result.Success)
            {
                return Unauthorized(result);
            }

            Response.Cookies.Append(CookieKeyNames.access_token, result.Token, CookieHelpers.GetAuthCookieOptions());
            return Ok(result);
        }
    }
}
