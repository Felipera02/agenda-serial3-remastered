using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Application.Interfaces.Repositories;
using AgendaSerial3.Domain.Entities;
using System.Text.RegularExpressions;


namespace AgendaSerial3.Application.UseCases.Users
{
    public class CreateUser(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;


        public async Task<UserResponseDTO> ExecuteAsync(UserRequestDTO dto)
        {
            var userWithSameUsername = await _userRepository.GetByUsernameAsync(dto.UserName);
            if (userWithSameUsername != null)
            {
                throw new Exception($"Usuário '{dto.UserName}' já existe.");
            }

            if (dto.UserName == null || !Regex.IsMatch(dto.UserName, @"^[a-zA-Z0-9_.]{3,20}$"))
            {
                throw new Exception($"O nome de usuário '{dto.UserName}' é inválido.");
            }

            var user = new User
            {
                Name = dto.Name,
                UserName = dto.UserName
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName
            };
        }
    }
}
