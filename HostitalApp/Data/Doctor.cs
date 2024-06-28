/*namespace HospitalApp.Data
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Clinic { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; } = new HashSet<Appointment>();
        public int DoctorId { get; internal set; }
    }
}
*/