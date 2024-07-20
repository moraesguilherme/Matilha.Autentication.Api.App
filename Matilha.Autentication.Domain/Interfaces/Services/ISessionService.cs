using Matilha.Autentication.Domain.Models.Entities;

public interface ISessionService
{
    Task CreateSessionAsync(Session session);
    Task InvalidateSessionAsync(string sessionId);
    Task<Session> GetSessionAsync(string sessionId);
}
