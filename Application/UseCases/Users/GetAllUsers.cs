using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.Interfaces.Repositories;

namespace AgendaSerial3.Application.UseCases.Users
{
    public class GetAllUsers(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        
        public async Task<List<UserResponseDTO>> ExecuteAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var usersDto = users.Select(u => new UserResponseDTO
            {
                Id = u.Id,
                Name = u.Name,
                UserName = u.UserName,
            }).ToList();
            return usersDto;
        }

    }
}
