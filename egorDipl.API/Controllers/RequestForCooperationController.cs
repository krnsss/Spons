using egorDipl.Data.Models;
using egorDipl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace egorDipl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestForCooperationController : ControllerBase
    {
        private readonly SponsorsDbContext _context;

        public RequestForCooperationController(SponsorsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestForCooperation>>> GetRequestsForCooperation()
        {
            return await _context.RequestForCooperation.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestForCooperation>> GetRequestForCooperation(int id)
        {
            var request = await _context.RequestForCooperation.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return request;
        }

        [HttpPost]
        public async Task<ActionResult<RequestForCooperation>> PostRequestForCooperation(RequestForCooperation request)
        {
            _context.RequestForCooperation.Add(request);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRequestForCooperation", new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestForCooperation(int id, RequestForCooperation request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestForCooperation(int id)
        {
            var request = await _context.RequestForCooperation.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.RequestForCooperation.Remove(request);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
