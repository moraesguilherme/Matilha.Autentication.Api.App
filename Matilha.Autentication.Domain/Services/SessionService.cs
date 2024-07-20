using Matilha.Autentication.Domain.Interfaces.Repositories;
using Matilha.Autentication.Domain.Models.Entities;

namespace Matilha.Autentication.Domain.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task CreateSessionAsync(Session session)
        {
            await _sessionRepository.AddSessionAsync(session);
        }

        public async Task<Session> GetSessionAsync(string sessionId)
        {
            return await _sessionRepository.GetSessionAsync(sessionId);
        }

        public async Task InvalidateSessionAsync(string sessionId)
        {
            await _sessionRepository.InvalidateSessionAsync(sessionId);
        }
    }
}
