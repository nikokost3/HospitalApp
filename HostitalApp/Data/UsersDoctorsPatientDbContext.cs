using Microsoft.EntityFrameworkCore;
using HospitalApp.Models;

namespace HospitalApp.Data
{
    public class UsersDoctorsPatientDbContext : DbContext
    {
        public UsersDoctorsPatientDbContext()
        {

        }

        public UsersDoctorsPatientDbContext(DbContextOptions<UsersDoctorsPatientDbContext> options) : base(options) 
        {
            
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");
                entity.HasIndex(e => e.Lastname, "IX_LASTNAME");
                entity.HasIndex(e => e.Username, "UQ_USERNAME").IsUnique();
                entity.HasIndex(e => e.Email, "UQ_EMAIL").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Username)
                    .HasMaxLength(50).HasColumnName("USERNAME");
                entity.Property(e => e.Password)
                    .HasMaxLength(512).HasColumnName("PASSWORD");
                entity.Property(e => e.Email)
                    .HasMaxLength(50).HasColumnName("EMAIL");
                entity.Property(e => e.Firstname)
                    .HasMaxLength(50).HasColumnName("FIRSTNAME");
                entity.Property(e => e.Lastname)
                    .HasMaxLength(50).HasColumnName("LASTNAME");
                entity.Property(e => e.UserRole)
                    .HasColumnName("USER_ROLE")
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("DOCTORS");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Clinic)
                    .HasMaxLength(50).HasColumnName("CLINIC");
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50).HasColumnName("PHONE_NUMBER");
                entity.HasOne(d => d.User)
                    .WithOne(p => p.Doctor)
                    .HasForeignKey<Doctor>(d => d.UserId)
                    .HasConstraintName("FK_DOCTORS_USERS");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("PATIENTS");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Clinic)
                    .HasMaxLength(50).HasColumnName("CLINIC");
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50).HasColumnName("PHONE_NUMBER");
                entity.HasOne(d => d.User).WithOne(p => p.Patient)
                      .HasForeignKey<Patient>(d => d.UserId)
                      .HasConstraintName("FK_PATIENTS_USERS");
                entity.HasMany(s => s.Appointments)
                    .WithMany(c => c.Patients)
                    .UsingEntity("PATIENTS_APPOINTMENTS");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("APPOINTMENT");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DoctorId).HasColumnName("DOCTOR_ID");
                entity.Property(e => e.Description)
                    .HasMaxLength(50).HasColumnName("DESCRIPTION");
                entity.Property(e => e.Day)
                    .HasMaxLength(50).HasColumnName("DATE_DAY");
                entity.HasOne(c => c.Doctor)
                    .WithMany(t => t.Appointments)
                    .HasForeignKey(c => c.DoctorId)
                    .HasConstraintName("FK_DOCTOR_APPOINTMENT");
            });
        }

    }
}
