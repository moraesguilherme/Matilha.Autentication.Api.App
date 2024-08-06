namespace Matilha.Autentication.Domain.Models.Entities
{
    public class UserPreferences
    {
        public int PreferenceId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
