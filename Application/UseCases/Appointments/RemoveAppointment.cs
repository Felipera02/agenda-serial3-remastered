using AgendaSerial3.Application.Interfaces.Repositories;

namespace AgendaSerial3.Application.UseCases.Appointments
{
    public class RemoveAppointment(IAppointmentRepository appointmentRepository)
    {
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;

        public async Task ExecuteAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment is null)
                throw new KeyNotFoundException("Compromisso não encontrado.");

            await _appointmentRepository.DeleteAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();
        }
    }
}
