using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.Interfaces.Repositories;

namespace AgendaSerial3.Application.UseCases.Appointments
{
    public class UpdateAppointment(IAppointmentRepository appointmentRepository, ICalendarRepository calendarRepository)
    {
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
        private readonly ICalendarRepository _calendarRepository = calendarRepository;

        public async Task<AppointmentResponseDTO> ExecuteAsync(int id, AppointmentRequestDTO dto)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment is null)
                throw new KeyNotFoundException("Compromisso não encontrado.");

            if (string.IsNullOrWhiteSpace(dto.Title) || dto.BeginDate == null || dto.EndDate == null || dto.CalendarId == 0)
                throw new ArgumentException("Todos os campos obrigatórios devem ser preenchidos.");

            if (dto.EndDate < dto.BeginDate)
                throw new ArgumentException("EndDate não pode ser anterior a BeginDate.");

            var calendar = await _calendarRepository.GetByIdAsync(dto.CalendarId);
            if (calendar is null)
                throw new KeyNotFoundException("Calendário não encontrado.");

            appointment.Title = dto.Title;
            appointment.Description = dto.Description;
            appointment.BeginDate = dto.BeginDate;
            appointment.EndDate = dto.EndDate;
            appointment.CalendarId = dto.CalendarId;

            await _appointmentRepository.UpdateAsync(appointment);
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
