using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using OpticalsWholesaleandRetail.DAL;
using OpticalsWholesaleandRetail.Utilities;
using OpticalsWholesaleandRetail.Models.DTO;
using OpticalsWholesaleandRetail.Models.Entity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpticalsWholesaleandRetail.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateApiController : ControllerBase
    {
      
            private readonly IUserRepo _service;
            private readonly MyDbContext _context;
            private readonly IConfiguration _config;
        private readonly JwtService _jwtService;

        public AuthenticateApiController(IUserRepo service, MyDbContext context, IConfiguration config, JwtService jwtService)
            {
                _service = service;
                _context = context;
                _config = config;
            _jwtService = jwtService;
        }

           
            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginRequest request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _service.LoginCheck(request);

            if (result.statusCode == 0)
            {
                return BadRequest(new { message = result.Message });
            }

            var token = _jwtService.GenerateToken(
                result.userId,
                result.userName,
                result.userTypeName
            );

            return Ok(new
            {
                token,
                user = result
            });
        }

            [HttpPost("logout")]
            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return Ok(new
                {
                    success = true,
                    message = "Logged out successfully"
                });
            }

            [HttpGet("me")]
            public IActionResult GetCurrentUser()
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(new { message = "Not logged in" });
                }

                return Ok(new
                {
                    userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    userName = User.FindFirst(ClaimTypes.Name)?.Value,
                    userType = User.FindFirst("UserType")?.Value
                });
            }
        }
}
