using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using egorDipl.Data.Models;
using egorDipl.Data;

namespace egorDipl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly SponsorsDbContext _context;

        public TagController(SponsorsDbContext context)
        {
            _context = context;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<TagsCategory>>> GetTagsCategories()
        {
            try
            {
                var categories = await _context.TagCategories
                    .Include(c => c.Tags)
                    .ToListAsync();

                return categories ?? new List<TagsCategory>();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tags>>> GetAllTags()
        {
            return await _context.Tags.ToListAsync();
        }

        // GET: api/Tag/event/{eventId}
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<IEnumerable<EventTag>>> GetTagsForEvent(int eventId)
        {
            return await _context.EventTags
                .Where(et => et.EventId == eventId)
                .Include(et => et.Tag)
                .ToListAsync();
        }

        // POST: api/Tag
        [HttpPost]
        public async Task<ActionResult<EventTag>> AddTagToEvent(EventTag eventTag)
        {
            _context.EventTags.Add(eventTag);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTagsForEvent), new { eventId = eventTag.EventId }, eventTag);
        }

        // DELETE: api/Tag/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTagFromEvent(int id)
        {
            var eventTag = await _context.EventTags.FindAsync(id);
            if (eventTag == null)
            {
                return NotFound();
            }

            _context.EventTags.Remove(eventTag);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Tag/{eventId}/{tagId}
        [HttpDelete("{eventId}/{tagId}")]
        public async Task<IActionResult> RemoveTagFromEvent(int eventId, int tagId)
        {
            var eventTag = await _context.EventTags
                .FirstOrDefaultAsync(et => et.EventId == eventId && et.TagId == tagId);

            if (eventTag == null)
            {
                return NotFound();
            }

            _context.EventTags.Remove(eventTag);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}