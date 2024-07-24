using Matilha.Autentication.Domain.Models.Entities;
using Matilha.Autentication.Domain.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Matilha.Autentication.Domain.Interfaces.Services;

namespace Matilha.Autentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            var result = await _authService.AuthenticateAsync(user.Username, user.PasswordHash);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}
