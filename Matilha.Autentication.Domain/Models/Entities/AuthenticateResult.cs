using Matilha.Autentication.Domain.Models.Entities;

public class AuthenticateResult
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public int UserId { get; set; }
    public int SessionId { get; set; }
    public List<UserPreferences> UserPreferences { get; set; }
}
