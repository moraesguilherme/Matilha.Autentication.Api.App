namespace Matilha.Autentication.Domain.Models.DTOs
{
    public class RegisterUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int? CompanyId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
