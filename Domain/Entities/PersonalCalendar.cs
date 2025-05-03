using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaSerial3.Domain.Entities
{
    public class PersonalCalendar
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public List<Appointment> Appointments { get; set; } = [];
    }
}
