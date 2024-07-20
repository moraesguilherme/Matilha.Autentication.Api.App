using Matilha.Autentication.Domain.Models.Entities;

public interface ISessionService
{
    Task CreateSessionAsync(Session session);
    Task InvalidateSessionAsync(Guid sessionId);
    Task<Session> GetSessionAsync(Guid sessionId);
}
