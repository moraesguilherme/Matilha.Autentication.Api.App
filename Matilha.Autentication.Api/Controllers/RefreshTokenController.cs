using Matilha.Autentication.Domain.Interfaces.Services;
using Matilha.Autentication.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Matilha.Autentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ILogger<RefreshTokenController> _logger;

        public RefreshTokenController(IRefreshTokenService refreshTokenService, ILogger<RefreshTokenController> logger)
        {
            _refreshTokenService = refreshTokenService;
            _logger = logger;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateRefreshToken([FromBody] GenerateRefreshTokenRequest request)
        {
            _logger.LogInformation(LogMessages.Messages["GenerateRefreshTokenAttempt"], request.UserId);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(request.UserId, request.CompanyId, request.SessionId);
            _logger.LogInformation(LogMessages.Messages["GenerateRefreshTokenSuccessful"], request.UserId);
            return Ok(refreshToken);
        }

        [HttpGet("validate/{token}")]
        public async Task<IActionResult> ValidateRefreshToken(string token)
        {
            _logger.LogInformation(LogMessages.Messages["ValidateRefreshTokenAttempt"]);
            var isValid = await _refreshTokenService.ValidateRefreshTokenAsync(token);

            if (!isValid)
            {
                _logger.LogWarning(LogMessages.Messages["ValidateRefreshTokenFailed"]);
                return Unauthorized();
            }

            _logger.LogInformation(LogMessages.Messages["ValidateRefreshTokenSuccessful"]);
            return Ok();
        }

        [HttpPost("invalidate/{token}")]
        public async Task<IActionResult> InvalidateRefreshToken(string token)
        {
            _logger.LogInformation(LogMessages.Messages["InvalidateRefreshTokenAttempt"]);
            await _refreshTokenService.InvalidateRefreshTokenAsync(token);
            _logger.LogInformation(LogMessages.Messages["InvalidateRefreshTokenSuccessful"]);
            return Ok();
        }
    }
}
