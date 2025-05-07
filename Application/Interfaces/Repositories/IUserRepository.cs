using AgendaSerial3.Domain.Entities;

namespace AgendaSerial3.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetByUsernameAsync(string? username);
    }
}
