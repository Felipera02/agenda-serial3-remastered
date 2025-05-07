using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.Interfaces.Repositories;

namespace AgendaSerial3.Application.UseCases.Appointments
{
    public class GetAppointmentByCalendar(IAppointmentRepository appointmentRepository)
    {
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;

        public async Task<List<AppointmentResponseDTO>> ExecuteAsync(int calendarId)
        {
            var appointments = await _appointmentRepository.GetWhere(a => a.CalendarId == calendarId);

            return appointments.Select(a => new AppointmentResponseDTO
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                BeginDate = a.BeginDate,
                EndDate = a.EndDate,
                CalendarId = a.CalendarId
            }).ToList();
        }
    }
}
