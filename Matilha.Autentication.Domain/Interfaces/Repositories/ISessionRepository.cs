using Matilha.Autentication.Domain.Models.Entities;

namespace Matilha.Autentication.Domain.Interfaces.Repositories
{
    public interface ISessionRepository
    {
        Task AddSessionAsync(Session session);
        Task InvalidateSessionAsync(string sessionId);
        Task<Session> GetSessionAsync(string sessionId);
    }
}
