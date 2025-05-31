using egorDipl.Data;
using egorDipl.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static UserController;

namespace egorDipl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(SponsorsDbContext Context) : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<TokenDto> Auth([FromBody] LoginModel loginModel)
        {
            var operationResult = new TokenDto();

            var user = await Context.User.FirstOrDefaultAsync(u => u.UniCode == loginModel.UniCode);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("001E33952B8942A5B07D88ECD3CD4719001E33952B8942A5B07D88ECD3CD4719"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UniCode.ToString()),
                new Claim("Role", user.Role.Name),
                new Claim("CompanyId", user.CompanyId.ToString()),
                new Claim("UserId", user.Id.ToString())
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: "Egor",
                audience: "https://localhost:7149",
                claims: [.. claims],
                expires: DateTime.Now.AddMinutes(50),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenDto() { AccessToken = tokenString };
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                if (loginModel == null || string.IsNullOrEmpty(loginModel.UniCode.ToString()) || string.IsNullOrEmpty(loginModel.Password))
                {
                    return BadRequest("Заполните все поля.");
                }

                var user = await Context.User.FirstOrDefaultAsync(u => u.UniCode == loginModel.UniCode);
                if (user == null)
                {
                    return Unauthorized("Неверный UniCode или пароль.");
                }

                var result = await Context.User.FirstOrDefaultAsync(u => u.UniCode == loginModel.UniCode && u.Password == loginModel.Password);
                if (result != null)
                {
                    return Ok("Авторизация успешна.");
                }
                else
                {
                    return Unauthorized("Неверный UniCode или пароль.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
        }
    }
}