using Matilha.Autentication.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Matilha.Autentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public RefreshTokenController(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateRefreshToken([FromBody] GenerateRefreshTokenRequest request)
        {
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(request.UserId, request.CompanyId, request.SessionId);
            return Ok(refreshToken);
        }

        [HttpGet("validate/{token}")]
        public async Task<IActionResult> ValidateRefreshToken(string token)
        {
            var isValid = await _refreshTokenService.ValidateRefreshTokenAsync(token);
            if (!isValid)
                return Unauthorized();

            return Ok();
        }

        [HttpPost("invalidate/{token}")]
        public async Task<IActionResult> InvalidateRefreshToken(string token)
        {
            await _refreshTokenService.InvalidateRefreshTokenAsync(token);
            return Ok();
        }
    }
}
