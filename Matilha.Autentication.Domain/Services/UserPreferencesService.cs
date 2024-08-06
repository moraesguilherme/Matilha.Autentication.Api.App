using Matilha.Autentication.Domain.Interfaces.Services;
using Matilha.Autentication.Domain.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Matilha.Autentication.Domain.Services
{
    public class UserPreferencesService : IUserPreferencesService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserPreferencesService> _logger;
        private readonly IConfiguration _configuration;

        public UserPreferencesService(HttpClient httpClient, ILogger<UserPreferencesService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<UserPreferences>> GetUserPreferencesAsync(int userId, string token)
        {
            try
            {
                string baseUrl = _configuration.GetValue<string>("ApiSettings:UserPreferencesUrl");
                string url = $"{baseUrl}/{userId}";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var userPreferences = JsonConvert.DeserializeObject<List<UserPreferences>>(content);
                    return userPreferences;
                }
                else
                {
                    _logger.LogWarning($"Falha ao consultar a API de preferências do usuário: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao consultar a API de preferências do usuário: {ex.Message}");
                return null;
            }
        }
    }
}
