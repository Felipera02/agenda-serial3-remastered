using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.Interfaces.Repositories;
using AgendaSerial3.Domain.Entities;

namespace AgendaSerial3.Application.UseCases.PersonalCalendars
{
    public class CreateCalendar(ICalendarRepository calendarRepository, IUserRepository userRepository)
    {
        private readonly ICalendarRepository _calendarRepository = calendarRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<PersonalCalendarResponseDTO> ExecuteAsync(PersonalCalendarRequestDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("O calendário deve ter um nome.");

            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user is null)
                throw new ArgumentException("Usuário não encontrado.");

            var calendar = new PersonalCalendar
            {
                Name = dto.Name,
                UserId = dto.UserId
            };

            await _calendarRepository.AddAsync(calendar);
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
