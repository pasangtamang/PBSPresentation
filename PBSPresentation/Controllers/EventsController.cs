using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBSPresentation.Data;

namespace PBSPresentation.Controllers
{
    [Route("api/[controller]/[action]")]
    public class EventsController : Controller
    {
        private readonly PbsPresentationContext _context;

        public EventsController(PbsPresentationContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Get all the data from event table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetAll()
        {
            return await _context.Events.ToListAsync();
        }

        /// <summary>
        /// Get single data
        /// </summary>
        /// <param name="id">int value</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var data = await _context.Events.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return data;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Event model)
        {
            var aa = Request;
            var bb = Response;
            _context.Events.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Event model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Events.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Events.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
          return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
