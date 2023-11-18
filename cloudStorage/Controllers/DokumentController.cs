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
    [Route("document")]
    public class DocumentController : ControllerBase
    {
        IDocumentService _documentService;
        IJwtTokenService _jwtTokenService;

        public DocumentController(
            IDocumentService documentService,
            IJwtTokenService jwtTokenService
            )
        {
            _documentService = documentService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<UploadDocumentResult>> UploadDocument([FromForm] UploadDocumentRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string jwtToken = Utils.CookieUtils.GetJwtTokenFromCookies(HttpContext.Request.Cookies);
            TokenData tokenData = _jwtTokenService.DecodeToken(jwtToken);

            var result = await _documentService.UploadDocument(request, tokenData);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}