/*namespace HospitalApp.Data
{
    public class Appointment
    {
        public int Id { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Patient>? Patients { get; } = new HashSet<Patient>();

        public int DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; }
    }
}
*/