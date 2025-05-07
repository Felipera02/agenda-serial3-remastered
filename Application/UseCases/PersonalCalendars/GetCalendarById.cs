using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.Interfaces.Repositories;

namespace AgendaSerial3.Application.UseCases.PersonalCalendars
{
    public class GetCalendarById(ICalendarRepository calendarRepository)
    {
        private readonly ICalendarRepository _calendarRepository = calendarRepository;

        public async Task<PersonalCalendarResponseDTO> ExecuteAsync(int id)
        {
            var calendar = await _calendarRepository.GetByIdAsync(id);
            if (calendar is null)
                throw new KeyNotFoundException("Calendário não encontrado.");

            return new PersonalCalendarResponseDTO
            {
                Id = calendar.Id,
                Name = calendar.Name,
                UserId = calendar.UserId
            };
        }
    }
}
