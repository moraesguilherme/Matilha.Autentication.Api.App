namespace Matilha.Autentication.Domain.Models.Entities
{
    public class RefreshToken
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int SessionId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsValid { get; set; }

        // Relationships
        public User User { get; set; }
        public Company Company { get; set; }
        public Session Session { get; set; }
    }
}
