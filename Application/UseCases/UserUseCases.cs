using AgendaSerial3.Domain.Entities;
using AgendaSerial3.Infrastructure.Repositories;

namespace AgendaSerial3.Application.UseCases
{
    public class UserUseCases(UserRepository repository)
    {
        private readonly UserRepository _repository = repository;

        public async void CreateUser(User user)
        {
            var addedUser = await _repository.AddAsync(user);
        }

}
