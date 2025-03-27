using egorDipl.Data.Models;
using egorDipl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace egorDipl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly SponsorsDbContext _context;

        public EventController(SponsorsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Event.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var eventItem = await _context.Event.FindAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return eventItem;
        }

        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event eventItem)
        {
            _context.Event.Add(eventItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEvent", new { id = eventItem.Id }, eventItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event eventItem)
        {
            if (id != eventItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventItem = await _context.Event.FindAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            _context.Event.Remove(eventItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
