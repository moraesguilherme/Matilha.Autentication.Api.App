using Matilha.Autentication.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Matilha.Autentication.Domain.Services;

namespace Matilha.Autentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ILogger<SessionController> _logger;

        public SessionController(ISessionService sessionService, ILogger<SessionController> logger)
        {
            _sessionService = sessionService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSession([FromBody] Session session)
        {
            _logger.LogInformation(LogMessages.Messages["CreateSessionAttempt"], session.UserId);
            await _sessionService.CreateSessionAsync(session);
            _logger.LogInformation(LogMessages.Messages["CreateSessionSuccessful"], session.UserId);
            return Ok();
        }

        [HttpGet("get/{sessionId}")]
        public async Task<IActionResult> GetSession(int sessionId)
        {
            _logger.LogInformation(LogMessages.Messages["GetSessionAttempt"], sessionId);
            var session = await _sessionService.GetSessionAsync(sessionId);

            if (session == null)
            {
                _logger.LogWarning(LogMessages.Messages["GetSessionNotFound"], sessionId);
                return NotFound();
            }

            _logger.LogInformation(LogMessages.Messages["GetSessionSuccessful"], sessionId);
            return Ok(session);
        }

        [HttpPost("invalidate/{sessionId}")]
        public async Task<IActionResult> InvalidateSession(int sessionId)
        {
            _logger.LogInformation(LogMessages.Messages["InvalidateSessionAttempt"], sessionId);
            await _sessionService.InvalidateSessionAsync(sessionId);
            _logger.LogInformation(LogMessages.Messages["InvalidateSessionSuccessful"], sessionId);
            return Ok();
        }
    }
}
