using Matilha.Autentication.Domain.Models.Entities;

public interface ISessionService
{
    Task<int> CreateSessionAsync(Session session);
    Task InvalidateSessionAsync(int sessionId);
    Task<Session> GetSessionAsync(int sessionId);
}
