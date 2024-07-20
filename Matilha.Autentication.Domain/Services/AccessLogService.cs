using Matilha.Autentication.Domain.Interfaces.Repositories;
using Matilha.Autentication.Domain.Interfaces.Services;
using Matilha.Autentication.Domain.Models.Entities;

namespace Matilha.Autentication.Domain.Services
{
    public class AccessLogService : IAccessLogService
    {
        private readonly IAccessLogRepository _accessLogRepository;

        public AccessLogService(IAccessLogRepository accessLogRepository)
        {
            _accessLogRepository = accessLogRepository;
        }

        public async Task LogAccessAsync(int userId, string action)
        {
            await _accessLogRepository.AddAccessLogAsync(new AccessLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Action = action,
                Timestamp = DateTime.UtcNow
            });
        }
    }
}
