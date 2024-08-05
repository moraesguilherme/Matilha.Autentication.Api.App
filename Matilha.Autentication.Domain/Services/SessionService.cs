using Matilha.Autentication.Domain.Interfaces.Repositories;
using Matilha.Autentication.Domain.Models.Entities;
using Microsoft.Extensions.Logging;

namespace Matilha.Autentication.Domain.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ILogger<SessionService> _logger;

        public SessionService(ISessionRepository sessionRepository, ILogger<SessionService> logger)
        {
            _sessionRepository = sessionRepository;
            _logger = logger;
        }

        public async Task<int> CreateSessionAsync(Session session)
        {
            _logger.LogInformation(LogMessages.Messages["CreateSessionServiceAttempt"], session.UserId);
            var sessionId = await _sessionRepository.AddSessionAsync(session);
            _logger.LogInformation(LogMessages.Messages["CreateSessionServiceSuccessful"], session.UserId);
            return sessionId;
        }

        public async Task<Session> GetSessionAsync(int sessionId)
        {
            _logger.LogInformation(LogMessages.Messages["GetSessionServiceAttempt"], sessionId);
            var session = await _sessionRepository.GetSessionAsync(sessionId);

            if (session == null)
            {
                _logger.LogWarning(LogMessages.Messages["GetSessionServiceNotFound"], sessionId);
                return null;
            }

            _logger.LogInformation(LogMessages.Messages["GetSessionServiceSuccessful"], sessionId);
            return session;
        }

        public async Task InvalidateSessionAsync(int sessionId)
        {
            _logger.LogInformation(LogMessages.Messages["InvalidateSessionServiceAttempt"], sessionId);
            var session = await GetSessionAsync(sessionId);

            if (session != null)
            {
                await _sessionRepository.InvalidateSessionAsync(sessionId);
                _logger.LogInformation(LogMessages.Messages["InvalidateSessionServiceSuccessful"], sessionId);
            }
        }
    }
}
