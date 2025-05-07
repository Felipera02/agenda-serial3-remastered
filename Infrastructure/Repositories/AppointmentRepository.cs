using AgendaSerial3.Application.Interfaces.Repositories;
using AgendaSerial3.Domain.Entities;
using AgendaSerial3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgendaSerial3.Infrastructure.Repositories
{
    public class AppointmentRepository(AgendaContext context)
        : GenericRepository<Appointment>(context), IAppointmentRepository
    { }
}
