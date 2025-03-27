using egorDipl.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static UserController;

namespace egorDipl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

                var user = await _userManager.FindByNameAsync(loginModel.UniCode.ToString());
                if (user == null)
                {
                    return Unauthorized("Неверный UniCode или пароль.");
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                if (result.Succeeded)
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