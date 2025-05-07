using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.Interfaces.Repositories;

namespace AgendaSerial3.Application.UseCases.Appointments
{
    public class GetAppointmentByUser(IAppointmentRepository appointmentRepository, ICalendarRepository calendarRepository)
    {
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
        private readonly ICalendarRepository _calendarRepository = calendarRepository;

        public async Task<List<AppointmentResponseDTO>> ExecuteAsync(int userId)
        {
            var calendars = await _calendarRepository.GetWhere(c => c.UserId == userId);
            var calendarIds = calendars.Select(c => c.Id).ToList();
            var appointments = await _appointmentRepository.GetWhere(a => calendarIds.Contains(a.CalendarId));

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
