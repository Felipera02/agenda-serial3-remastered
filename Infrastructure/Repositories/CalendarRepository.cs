using AgendaSerial3.Domain.Entities;
using AgendaSerial3.Infrastructure.Settings;

namespace AgendaSerial3.Infrastructure.Repositories
{
    public class CalendarRepository(AgendaContext context) : GenericRepository<PersonalCalendar>(context)
    {
    }
}
