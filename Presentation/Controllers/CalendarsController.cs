using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgendaSerial3.Domain.Entities;
using AgendaSerial3.Infrastructure.Data;

namespace AgendaSerial3.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarsController : ControllerBase
    {
        private readonly AgendaContext _context;

        public CalendarsController(AgendaContext context)
        {
            _context = context;
        }

        // GET: api/Calendars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalCalendar>>> GetCalendars()
        {
            return await _context.Calendars.ToListAsync();
        }

        // GET: api/Calendars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalCalendar>> GetPersonalCalendar(int id)
        {
            var personalCalendar = await _context.Calendars.FindAsync(id);

            if (personalCalendar == null)
            {
                return NotFound();
            }

            return personalCalendar;
        }

        // PUT: api/Calendars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonalCalendar(int id, PersonalCalendar personalCalendar)
        {
            if (id != personalCalendar.Id)
            {
                return BadRequest();
            }

            _context.Entry(personalCalendar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalCalendarExists(id))
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

        // POST: api/Calendars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonalCalendar>> PostPersonalCalendar(PersonalCalendar personalCalendar)
        {
            //var user = await _context.Users.FindAsync(personalCalendar.UserId);
            //if (user == null)
            //{
            //    return BadRequest();
            //}
            _context.Calendars.Add(personalCalendar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonalCalendar", new { id = personalCalendar.Id }, personalCalendar);
        }

        // DELETE: api/Calendars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonalCalendar(int id)
        {
            var personalCalendar = await _context.Calendars.FindAsync(id);
            if (personalCalendar == null)
            {
                return NotFound();
            }

            _context.Calendars.Remove(personalCalendar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonalCalendarExists(int id)
        {
            return _context.Calendars.Any(e => e.Id == id);
        }
    }
}
