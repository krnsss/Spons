using egorDipl.Data;
using egorDipl.Data.Models;
using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly SponsorsDbContext _context;

    public UserService(SponsorsDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.User
                             .Include(u => u.Role)
                             .Include(u => u.Company)
                             .FirstOrDefaultAsync(u => u.Id == userId);
    }
}
