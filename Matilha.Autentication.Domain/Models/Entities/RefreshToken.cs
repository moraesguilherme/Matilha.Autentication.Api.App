namespace Matilha.Autentication.Domain.Models.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsValid { get; set; }
    }
}
