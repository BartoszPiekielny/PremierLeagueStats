using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PremierLeagueStats.DTOs;
using PremierLeagueStats.Models;
using PremierLeagueStats.Services;

namespace PremierLeagueStats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtService _jwtService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            JwtService jwtService,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(
                user,
                dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(
                    new IdentityRole("User"));
            }

            await _userManager.AddToRoleAsync(
                user,
                "User");

            return Ok(new
            {
                Message = "User created successfully"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user =
                await _userManager.FindByEmailAsync(
                    dto.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            var validPassword =
                await _userManager.CheckPasswordAsync(
                    user,
                    dto.Password);

            if (!validPassword)
            {
                return Unauthorized();
            }

            var token =
                await _jwtService.GenerateToken(
                    user,
                    _userManager);

            return Ok(new
            {
                token
            });
        }
    }
}