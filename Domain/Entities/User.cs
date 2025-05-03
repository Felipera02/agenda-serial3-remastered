using Microsoft.EntityFrameworkCore;

namespace AgendaSerial3.Domain.Entities
{
    [Index(nameof(UserName))]
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }

        public List<PersonalCalendar> Calendars { get; set; } = [];
    }
}
