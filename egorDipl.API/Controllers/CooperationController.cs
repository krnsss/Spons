using egorDipl.Data.Models;
using egorDipl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace egorDipl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CooperationController : ControllerBase
    {
        private readonly SponsorsDbContext _context;

        public CooperationController(SponsorsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cooperation>>> GetCooperations()
        {
            return await _context.Cooperation.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cooperation>> GetCooperation(int id)
        {
            var cooperation = await _context.Cooperation.FindAsync(id);
            if (cooperation == null)
            {
                return NotFound();
            }
            return cooperation;
        }

        [HttpPost]
        public async Task<ActionResult<Cooperation>> PostCooperation(Cooperation cooperation)
        {
            _context.Cooperation.Add(cooperation);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCooperation", new { id = cooperation.Id }, cooperation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCooperation(int id, Cooperation cooperation)
        {
            if (id != cooperation.Id)
            {
                return BadRequest();
            }

            _context.Entry(cooperation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCooperation(int id)
        {
            var cooperation = await _context.Cooperation.FindAsync(id);
            if (cooperation == null)
            {
                return NotFound();
            }

            _context.Cooperation.Remove(cooperation);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
