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
    }
}
