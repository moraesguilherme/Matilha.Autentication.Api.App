using Matilha.Autentication.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Matilha.Autentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSession([FromBody] Session session)
        {
            await _sessionService.CreateSessionAsync(session);
            return Ok();
        }

        [HttpGet("get/{sessionId}")]
        public async Task<IActionResult> GetSession(string sessionId)
        {
            var session = await _sessionService.GetSessionAsync(sessionId);
            if (session == null)
                return NotFound();

            return Ok(session);
        }

        [HttpPost("invalidate/{sessionId}")]
        public async Task<IActionResult> InvalidateSession(string sessionId)
        {
            await _sessionService.InvalidateSessionAsync(sessionId);
            return Ok();
        }
    }
}
