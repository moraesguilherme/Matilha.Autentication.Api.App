namespace Matilha.Autentication.Domain.Models.Entities
{
    public class Session
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsValid { get; set; }
        public int RefreshTokenId { get; set; }
    }
}
