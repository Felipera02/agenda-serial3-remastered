using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.Interfaces.Repositories;

namespace AgendaSerial3.Application.UseCases.PersonalCalendars
{
    public class GetCalendarByUser(ICalendarRepository calendarRepository)
    {
        private readonly ICalendarRepository _calendarRepository = calendarRepository;

        public async Task<List<PersonalCalendarResponseDTO>> ExecuteAsync(int userId)
        {
            var calendars = await _calendarRepository.GetWhere(c => c.UserId == userId);
            return calendars.Select(c => new PersonalCalendarResponseDTO
            {
                Id = c.Id,
                Name = c.Name,
                UserId = c.UserId
            }).ToList();
        }
    }
}
