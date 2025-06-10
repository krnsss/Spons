using egorDipl.Data.Models;
using egorDipl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly SponsorsDbContext _context;

    public UserController(SponsorsDbContext context)
    {
        _context = context;
    }

    // Получить всех пользователей
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.User.ToListAsync();
    }

    // Авторизация пользователя по UniCode и Password
    [HttpPost("Login")]
    public async Task<ActionResult<User>> Login([FromBody] LoginModel loginData)
    {
        if (loginData == null || loginData.UniCode == 0 || string.IsNullOrEmpty(loginData.Password))
        {
            return BadRequest("UniCode и пароль обязательны.");
        }

        var user = await _context.User.FirstOrDefaultAsync(u => u.UniCode == loginData.UniCode);
        if (user == null)
        {
            return Unauthorized(new { message = "Пользователь не найден." });
        }

        if (user.Password != loginData.Password)
        {
            return Unauthorized(new { message = "Неверный пароль." });
        }

        return Ok(user);
    }

    public class LoginModel
    {
        public int UniCode { get; set; }
        public string Password { get; set; }
    }

    // Регистрация пользователя
    [HttpPost]
    public async Task<ActionResult<User>> PostUser([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("User is null");
        }

        var role = await _context.StaffRole.FindAsync(user.RoleId);
        if (role == null)
        {
            return NotFound("Роль не найдена.");
        }

        if (user.CompanyId.HasValue)
        {
            var company = await _context.Company.FindAsync(user.CompanyId);
            if (company == null)
            {
                return NotFound("Компания не найдена.");
            }
        }

        _context.User.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // Обновить пользователя
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return BadRequest("ID пользователя не совпадает.");
        }

        var existingUser = await _context.User.FindAsync(id);
        if (existingUser == null)
        {
            return NotFound("Пользователь не найден.");
        }
        else
        {
            existingUser.Surname = user.Surname;
            existingUser.Name = user.Name;
            existingUser.Patronymic = user.Patronymic;
            existingUser.Post = user.Post;
            existingUser.RoleId = user.RoleId;

            await _context.SaveChangesAsync();
            return Ok("Запись обновлена");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Console.WriteLine($"Запрос на удаление пользователя с ID {id}");

        var user = await _context.User.FindAsync(id);
        if (user == null)
        {
            Console.WriteLine("Пользователь не найден");
            return NotFound();
        }

        _context.User.Remove(user);
        await _context.SaveChangesAsync();

        Console.WriteLine("Пользователь удален");
        return NoContent();
    }

    // UserController.cs
    [HttpGet("company/{companyId}")]
    public async Task<ActionResult<List<User>>> GetUsersByCompany(int companyId)
    {
        return await _context.User
            .Include(u => u.Company)
            .Include(u => u.Role)
            .Where(u => u.CompanyId == companyId)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.User
            .Include(u => u.Company)
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }
}
