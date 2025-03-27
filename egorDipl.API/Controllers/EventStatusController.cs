using egorDipl.Data.Models;
using egorDipl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace egorDipl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventStatusController : ControllerBase
    {
        private readonly SponsorsDbContext _context;

        public EventStatusController(SponsorsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventStatus>>> GetEventStatuses()
        {
            return await _context.EventStatus.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventStatus>> GetEventStatus(int id)
        {
            var eventStatus = await _context.EventStatus.FindAsync(id);
            if (eventStatus == null)
            {
                return NotFound();
            }
            return eventStatus;
        }

        [HttpPost]
        public async Task<ActionResult<EventStatus>> PostEventStatus(EventStatus eventStatus)
        {
            _context.EventStatus.Add(eventStatus);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEventStatus", new { id = eventStatus.Id }, eventStatus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventStatus(int id, EventStatus eventStatus)
        {
            if (id != eventStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventStatus(int id)
        {
            var eventStatus = await _context.EventStatus.FindAsync(id);
            if (eventStatus == null)
            {
                return NotFound();
            }

            _context.EventStatus.Remove(eventStatus);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
