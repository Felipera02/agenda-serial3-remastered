using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.Interfaces.Repositories;
using AgendaSerial3.Domain.Entities;

namespace AgendaSerial3.Application.UseCases.Appointments
{
    public class CreateAppointment(IAppointmentRepository appointmentRepository, ICalendarRepository calendarRepository)
    {
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
        private readonly ICalendarRepository _calendarRepository = calendarRepository;

        public async Task<AppointmentResponseDTO> ExecuteAsync(AppointmentRequestDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || dto.BeginDate == null || dto.EndDate == null || dto.CalendarId == 0)
                throw new ArgumentException("Todos os campos obrigatórios devem ser preenchidos.");

            if (dto.EndDate < dto.BeginDate)
                throw new ArgumentException("As datas para ínicio e fim do compromisso não são válidas.");

            var calendar = await _calendarRepository.GetByIdAsync(dto.CalendarId);
            if (calendar is null)
                throw new KeyNotFoundException("Calendário não encontrado.");

            var appointment = new Appointment
            {
                Title = dto.Title,
                Description = dto.Description,
                BeginDate = dto.BeginDate,
                EndDate = dto.EndDate,
                CalendarId = dto.CalendarId
            };

            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return new AppointmentResponseDTO
            {
                Id = appointment.Id,
                Title = appointment.Title,
                Description = appointment.Description,
                BeginDate = appointment.BeginDate,
                EndDate = appointment.EndDate,
                CalendarId = appointment.CalendarId
            };
        }
    }
}
