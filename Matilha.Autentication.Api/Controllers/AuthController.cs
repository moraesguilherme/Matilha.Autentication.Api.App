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
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var token = await _authService.AuthenticateAsync(user.Username, user.PasswordHash);

            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            var user = await _authService.RegisterAsync(registerUser);

            if (user == null)
                return BadRequest("Username already exists.");

            var token = await _authService.AuthenticateAsync(user.Username, registerUser.Password);

            return Ok(new { Token = token, user.UserId, user.CompanyId });
        }
    }
}
