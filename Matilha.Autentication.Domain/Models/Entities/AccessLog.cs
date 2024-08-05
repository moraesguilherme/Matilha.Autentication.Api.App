namespace Matilha.Autentication.Domain.Models.Entities
{
    public class AccessLog
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int SessionId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
