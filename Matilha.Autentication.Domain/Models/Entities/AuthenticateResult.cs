public class AuthenticateResult
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public int UserId { get; set; }
    public int SessionId { get; set; }
}
