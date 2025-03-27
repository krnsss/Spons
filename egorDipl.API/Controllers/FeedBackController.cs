using egorDipl.Data.Models;
using egorDipl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace egorDipl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly SponsorsDbContext _context;

        public FeedBackController(SponsorsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedBack>>> GetFeedBacks()
        {
            return await _context.FeedBack.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedBack>> GetFeedBack(int id)
        {
            var feedBack = await _context.FeedBack.FindAsync(id);
            if (feedBack == null)
            {
                return NotFound();
            }
            return feedBack;
        }

        [HttpPost]
        public async Task<ActionResult<FeedBack>> PostFeedBack(FeedBack feedBack)
        {
            _context.FeedBack.Add(feedBack);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFeedBack", new { id = feedBack.Id }, feedBack);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedBack(int id, FeedBack feedBack)
        {
            if (id != feedBack.Id)
            {
                return BadRequest();
            }

            _context.Entry(feedBack).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedBack(int id)
        {
            var feedBack = await _context.FeedBack.FindAsync(id);
            if (feedBack == null)
            {
                return NotFound();
            }

            _context.FeedBack.Remove(feedBack);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
