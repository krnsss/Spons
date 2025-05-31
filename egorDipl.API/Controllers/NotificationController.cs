using egorDipl.Data;
using egorDipl.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace egorDipl.API.Controllers
{
    namespace egorDipl.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class NotificationController : ControllerBase
        {
            private readonly SponsorsDbContext _context;

            public NotificationController(SponsorsDbContext context) => _context = context;

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
            {
                return await _context.Notification.ToListAsync();
            }

            /// <summary>
            /// Получение уведомлений по ID компании
            /// </summary>
            /// <param name="id">ID компании</param>
            /// <returns></returns>
            [HttpGet("{id}")]
            public async Task<ActionResult<List<Notification>>> GetNotification(int id)
            {
                var notifications = await _context.Notification
                    .Where(x => x.CompanyId == id)
                    .ToListAsync();

                if (notifications == null)
                    return NotFound();

                return notifications;
            }

            [HttpPost]
            public async Task<ActionResult<Notification>> PostNotification(Notification notification)
            {
                _context.Notification.Add(notification);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetNotification", new { id = notification.Id }, notification);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> PutNotification(int id, Notification notification)
            {
                if (id != notification.Id)
                    return BadRequest();

                _context.Entry(notification).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Notification.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteNotification(int id)
            {
                Console.WriteLine($"Запрос на удаление компании с ID {id}");

                var notification = await _context.Notification.FindAsync(id);
                if (notification == null)
                {
                    Console.WriteLine("Компания не найдена");
                    return NotFound();
                }

                _context.Notification.Remove(notification);
                await _context.SaveChangesAsync();

                Console.WriteLine("Компания удалена");
                return NoContent();
            }
        }
    }
}
