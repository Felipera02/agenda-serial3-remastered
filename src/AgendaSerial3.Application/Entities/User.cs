using Microsoft.AspNetCore.Identity;

namespace AgendaSerial3.Application.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public virtual ICollection<PersonalCalendar> PersonalCalendars { get; set; } = [];
    public virtual ICollection<Appointment> Appointments { get; set; } = [];
}
