namespace AgendaSerial3.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int CalendarId { get; set; }
        public PersonalCalendar? Calendar { get; set; }
    }
}
