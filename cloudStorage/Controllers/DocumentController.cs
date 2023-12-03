using System.Text.Json;
using CloudStorage.Constants;
using CloudStorage.DTOs;
using CloudStorage.Interfaces;
using CloudStorage.Models;
using CloudStorage.Utils;
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

            string jwtTokenFromCookie = HttpContext.Request.Cookies[CookieKeyNames.access_token];
            TokenData tokenData = _jwtTokenService.DecodeToken(jwtTokenFromCookie);

            var result = await _documentService.UploadDocument(request, tokenData);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllDocumentsByOwnerId()
        {
            string jwtTokenFromCookie = HttpContext.Request.Cookies[CookieKeyNames.access_token];
            TokenData tokenData = _jwtTokenService.DecodeToken(jwtTokenFromCookie);

            var documents = await _documentService.GetAllDocumentsByOwnerId(tokenData.UserId);

            return Ok(documents);
        }
    }
}