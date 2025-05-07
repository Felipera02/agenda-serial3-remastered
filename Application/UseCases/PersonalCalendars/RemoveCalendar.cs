using AgendaSerial3.Application.Interfaces.Repositories;

namespace AgendaSerial3.Application.UseCases.PersonalCalendars
{
    public class RemoveCalendar(ICalendarRepository calendarRepository)
    {
        private readonly ICalendarRepository _calendarRepository = calendarRepository;

        public async Task ExecuteAsync(int id)
        {
            var calendar = await _calendarRepository.GetByIdAsync(id);
            if (calendar is null)
                throw new KeyNotFoundException("Calendário não encontrado.");

            await _calendarRepository.DeleteAsync(calendar);
            await _calendarRepository.SaveChangesAsync();
        }
    }
}
