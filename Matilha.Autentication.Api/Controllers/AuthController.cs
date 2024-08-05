using Matilha.Autentication.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Matilha.Autentication.Domain.Interfaces.Services;
using Matilha.Autentication.Domain.Services;

namespace Matilha.Autentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            _logger.LogInformation(LogMessages.Messages["LoginAttempt"], user.Username);
            var result = await _authService.AuthenticateAsync(user.Username, user.PasswordHash);

            if (result == null)
            {
                _logger.LogWarning(LogMessages.Messages["LoginFailed"], user.Username);
                return Unauthorized();
            }

            _logger.LogInformation(LogMessages.Messages["LoginSuccessful"], user.Username);
            return Ok(result);
        }
    }
}
