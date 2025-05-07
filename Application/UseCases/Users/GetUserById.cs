using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.Interfaces.Repositories;

namespace AgendaSerial3.Application.UseCases.Users
{
    public class GetUserById(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserResponseDTO?> ExecuteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                throw new Exception("Usuário não encontrado");

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName
            };
        }
    }
}
