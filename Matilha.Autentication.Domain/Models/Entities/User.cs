namespace Matilha.Autentication.Domain.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public Company Company { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<AccessLog> AccessLogs { get; set; }
    }
}
