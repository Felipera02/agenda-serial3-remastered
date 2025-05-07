using Microsoft.AspNetCore.Mvc;
using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.UseCases.Appointments;

namespace AgendaSerial3.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController(
        CreateAppointment createAppointment,
        GetAppointmentById getAppointmentById,
        GetAppointmentByCalendar getAppointmentByCalendar,
        GetAppointmentByUser getAppointmentByUser,
        UpdateAppointment updateAppointment,
        RemoveAppointment removeAppointment) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentRequestDTO dto)
        {
            try
            {
                var result = await createAppointment.ExecuteAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await getAppointmentById.ExecuteAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("calendar/{calendarId}")]
        public async Task<IActionResult> GetByCalendar(int calendarId)
        {
             var result = await getAppointmentByCalendar.ExecuteAsync(calendarId);
             return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var result = await getAppointmentByUser.ExecuteAsync(userId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentRequestDTO dto)
        {
            try
            {
                var result = await updateAppointment.ExecuteAsync(id, dto);
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
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await removeAppointment.ExecuteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
