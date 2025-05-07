namespace AgendaSerial3.Application.DTOs
{
    public class AppointmentResponseDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CalendarId { get; set; }
    }
}
