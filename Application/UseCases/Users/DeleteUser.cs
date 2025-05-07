using AgendaSerial3.Application.Interfaces.Repositories;

namespace AgendaSerial3.Application.UseCases.Users
{
    public class DeleteUser(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task ExecuteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                throw new Exception("Usuário não encontrado");

            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}
