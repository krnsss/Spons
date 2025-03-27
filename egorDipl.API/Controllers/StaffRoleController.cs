using egorDipl.Data.Models;
using egorDipl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace egorDipl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffRoleController : ControllerBase
    {
        private readonly SponsorsDbContext _context;

        public StaffRoleController(SponsorsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffRole>>> GetStaffRoles()
        {
            return await _context.StaffRole.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffRole>> GetStaffRole(int id)
        {
            var staffRole = await _context.StaffRole.FindAsync(id);
            if (staffRole == null)
            {
                return NotFound();
            }
            return staffRole;
        }

        [HttpPost]
        public async Task<ActionResult<StaffRole>> PostStaffRole(StaffRole staffRole)
        {
            _context.StaffRole.Add(staffRole);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetStaffRole", new { id = staffRole.Id }, staffRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffRole(int id, StaffRole staffRole)
        {
            if (id != staffRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(staffRole).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffRole(int id)
        {
            var staffRole = await _context.StaffRole.FindAsync(id);
            if (staffRole == null)
            {
                return NotFound();
            }

            _context.StaffRole.Remove(staffRole);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
