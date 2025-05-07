using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.Interfaces.Repositories;

namespace AgendaSerial3.Application.UseCases.PersonalCalendars
{
    public class UpdateCalendar(ICalendarRepository calendarRepository)
    {
        private readonly ICalendarRepository _calendarRepository = calendarRepository;

        public async Task<PersonalCalendarResponseDTO> ExecuteAsync(int id, PersonalCalendarRequestDTO dto)
        {
            var calendar = await _calendarRepository.GetByIdAsync(id);
            if (calendar is null)
                throw new KeyNotFoundException("Calendário não encontrado.");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                calendar.Name = dto.Name;

            await _calendarRepository.UpdateAsync(calendar);
            await _calendarRepository.SaveChangesAsync();

            return new PersonalCalendarResponseDTO
            {
                Id = calendar.Id,
                Name = calendar.Name,
                UserId = calendar.UserId
            };
        }
    }
}
