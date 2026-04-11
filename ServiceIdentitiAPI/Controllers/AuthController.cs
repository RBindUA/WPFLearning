using Microsoft.AspNetCore.Mvc;
using ServiceIdentityAPI.Models;
using ServiceIdentityAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceIdentityAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginService _loginService;
        private readonly TokenService _tokenService;


        public AuthController(LoginService loginService, TokenService tokenService)
        {
            _loginService = loginService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _loginService.AuthenticateAsync(request.Email, request.Password);

            if (user == null)
                return StatusCode(401, new { message = "Invalid email or password" });

            var token = _tokenService.GenerateToken(user, request.Email);

            return Ok(new AuthResponse
            {
                Token = token,
                BusinessEntityID = user.BusinessEntityID
            });
        }
    }
}
