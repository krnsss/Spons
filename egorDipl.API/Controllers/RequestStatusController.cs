using egorDipl.Data.Models;
using egorDipl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace egorDipl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestStatusController : ControllerBase
    {
        private readonly SponsorsDbContext _context;

        public RequestStatusController(SponsorsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestStatus>>> GetRequestStatuses()
        {
            return await _context.RequestStatus.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestStatus>> GetRequestStatus(int id)
        {
            var requestStatus = await _context.RequestStatus.FindAsync(id);
            if (requestStatus == null)
            {
                return NotFound();
            }
            return requestStatus;
        }

        [HttpPost]
        public async Task<ActionResult<RequestStatus>> PostRequestStatus(RequestStatus requestStatus)
        {
            _context.RequestStatus.Add(requestStatus);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRequestStatus", new { id = requestStatus.Id }, requestStatus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestStatus(int id, RequestStatus requestStatus)
        {
            if (id != requestStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestStatus(int id)
        {
            var requestStatus = await _context.RequestStatus.FindAsync(id);
            if (requestStatus == null)
            {
                return NotFound();
            }

            _context.RequestStatus.Remove(requestStatus);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
