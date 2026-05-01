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
        private readonly RegisterService _registerService;


        public AuthController(LoginService loginService, TokenService tokenService, RegisterService registerService)
        {
            _loginService = loginService;
            _tokenService = tokenService;
            _registerService = registerService;
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
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO request)
        {
            if (request == null)
            {
                return BadRequest("Registration data is missing");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool isRegistered = await _registerService.RegisterAsync(request);

                if (isRegistered)
                {
                    return Ok(new { message = "User registered successfully" });
                }
                return BadRequest(new { message = "Registration failed. Email may be already in use" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error during registration", detail = ex.Message });
            }
        }
    }
}
