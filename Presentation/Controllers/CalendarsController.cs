using Microsoft.AspNetCore.Mvc;
using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.UseCases.PersonalCalendars;

namespace AgendaSerial3.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarsController(
        CreateCalendar createCalendar,
        GetCalendarById getCalendarById,
        GetCalendarByUser getCalendarByUser,
        UpdateCalendar updateCalendar,
        RemoveCalendar removeCalendar) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonalCalendarRequestDTO dto)
        {
            try
            {
                var result = await createCalendar.ExecuteAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await getCalendarById.ExecuteAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var result = await getCalendarByUser.ExecuteAsync(userId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonalCalendarRequestDTO dto)
        {
            try
            {
                var result = await updateCalendar.ExecuteAsync(id, dto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await removeCalendar.ExecuteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
