using CloudStorage.DTOs;
using CloudStorage.Helpers;
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
                return BadRequest(
                    new UploadDokumentResult()
                    {
                        Success = false,
                        Errors = {
                            "Invalid input data"
                        }
                    }
                );
            }

            string jwtToken = HttpHelpers.GetJwtTokenFromHeaders(HttpContext.Request.Headers);
            TokenData tokenData = _jwtTokenService.DecodeToken(jwtToken);

            var result = await _dokumentService.UploadDokument(request, tokenData);

            if (result.Errors != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}