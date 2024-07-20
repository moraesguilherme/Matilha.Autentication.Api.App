namespace Matilha.Autentication.Domain.Interfaces.Services
{
    public interface IAccessLogService
    {
        Task LogAccessAsync(int userId, string action);
    }
}
