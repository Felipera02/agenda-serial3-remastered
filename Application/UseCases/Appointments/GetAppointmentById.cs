using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.Interfaces.Repositories;

namespace AgendaSerial3.Application.UseCases.Appointments
{
    public class GetAppointmentById(IAppointmentRepository appointmentRepository)
    {
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;

        public async Task<AppointmentResponseDTO> ExecuteAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment is null)
                throw new KeyNotFoundException("Compromisso não encontrado.");

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
