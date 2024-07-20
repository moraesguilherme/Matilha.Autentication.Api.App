namespace Matilha.Autentication.Domain.Models.Entities
{
    public class AccessLog
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
