using Matilha.Autentication.Domain.Models.Entities;

namespace Matilha.Autentication.Domain.Interfaces.Repositories
{
    public interface ISessionRepository
    {
        Task<int> AddSessionAsync(Session session);
        Task InvalidateSessionAsync(int sessionId);
        Task<Session> GetSessionAsync(int sessionId);
    }
}
