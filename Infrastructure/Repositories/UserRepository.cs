using AgendaSerial3.Application.Interfaces.Repositories;
using AgendaSerial3.Domain.Entities;
using AgendaSerial3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgendaSerial3.Infrastructure.Repositories
{
    public class UserRepository(AgendaContext context)
        : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetByUsernameAsync(string? username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
