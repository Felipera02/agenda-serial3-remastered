namespace AgendaSerial3.Application.Entities;

public class PersonalCalendar
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual User User { get; set; } = null!;
    public virtual ICollection<Appointment> Appointments { get; set; } = [];
}
