using Matilha.Autentication.Domain.Interfaces.Repositories;
using Matilha.Autentication.Domain.Interfaces.Services;
using Matilha.Autentication.Domain.Models.Entities;

namespace Matilha.Autentication.Domain.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IAccessLogService _accessLogService;

        public SessionService(ISessionRepository sessionRepository, IAccessLogService accessLogService)
        {
            _sessionRepository = sessionRepository;
            _accessLogService = accessLogService;
        }

        public async Task<int> CreateSessionAsync(Session session)
        {
            return await _sessionRepository.AddSessionAsync(session);
        }

        public async Task<Session> GetSessionAsync(int sessionId)
        {
            return await _sessionRepository.GetSessionAsync(sessionId);
        }

        public async Task InvalidateSessionAsync(int sessionId)
        {
            var session = await GetSessionAsync(sessionId);

            await _accessLogService.LogAccessAsync(session.UserId, session.CompanyId, sessionId, "Logout");

            await _sessionRepository.InvalidateSessionAsync(sessionId);
        }
    }
}
