namespace Matilha.Autentication.Domain.Models.Entities
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
