using CloudStorage.DTOs;
using CloudStorage.Interfaces;
using CloudStorage.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorage.Contorllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("dokument")]
    public class DokumentController : ControllerBase
    {
        IDokumentService _dokumentService;
        IJwtTokenService _jwtTokenService;

        public DokumentController(
            IDokumentService dokumentService,
            IJwtTokenService jwtTokenService
            )
        {
            _dokumentService = dokumentService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<UploadDokumentResult>> UploadDokument([FromForm] UploadDokumentRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string jwtToken = Utils.CookieUtils.GetJwtTokenFromCookies(HttpContext.Request.Cookies);
            TokenData tokenData = _jwtTokenService.DecodeToken(jwtToken);

            var result = await _dokumentService.UploadDokument(request, tokenData);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}