using AgendaSerial3.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace AgendaSerial3.Infrastructure.Data.Repository;

public class CategoryRepository(AgendaDbContext context) : GenericRepository <Category>(context)
{
    public async Task<Category?> GetCategoryAndAppointmentsAsync(int categoryId, string userId)
    {
        return await _context.Categories
                .Include(c => c.Appointments)
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);
    }

    public override async Task DeleteAsync(Category category)
    {
        if (category.Appointments.Any())
        {
            _context.Appointments.RemoveRange(category.Appointments);
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}
