/*namespace HospitalApp.Data
{
    public class Patient
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? Clinic { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; } = null!;

        public virtual ICollection<Appointment>? Appointments { get; } = new HashSet<Appointment>();

        public override string? ToString()
        {
            return $"{User!.Firstname}, {User.Lastname}, {Clinic}";
        }
    }
}
*/