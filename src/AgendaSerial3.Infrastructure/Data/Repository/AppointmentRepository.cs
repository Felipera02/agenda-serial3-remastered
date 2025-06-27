using AgendaSerial3.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace AgendaSerial3.Infrastructure.Data.Repository;

public class AppointmentRepository(AgendaDbContext context) : GenericRepository<Appointment>(context) 
{
    public async Task<IEnumerable<Appointment>> GetAppointmentsAndCategoriesByUserIdAndDatesAsync(string userId, DateTime startDate, DateTime endDate)
    {
        var appointments = await _context.Appointments
            .Include(a => a.Category)
            .Where(a => a.UserId == userId &&
                       a.StartDateTime >= startDate &&
                       a.StartDateTime <= endDate)
            .ToListAsync();

        return appointments;
    }

    public async Task<Appointment?> GetAppointmentAndCategoriesAsync(int appointmentId, string userId)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Category)
            .FirstOrDefaultAsync(a => a.Id == appointmentId && a.UserId == userId);

        return appointment;
    }
}
