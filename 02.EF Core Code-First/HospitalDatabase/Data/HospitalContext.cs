using System.IO;
using System.Linq;
using System.Text;
using HospitalDatabase.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> PatientMedicaments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string path = @"C:\Users\Ss\Documents\Visual Studio 2017\Projects\C# DATABASES ADVANCED - ENTITY FRAMEWORK\02.EF Core Code-First\connection.txt";
                var connection = File.ReadAllLines(path, Encoding.UTF8).FirstOrDefault();

                builder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(250);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(80);

                entity.Property(e => e.HasInsurance)
                    .HasDefaultValue(true);

            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .IsRequired(true);

                entity.Property(e => e.Specialty)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Visitation>(entity =>
            {
                entity.HasKey(e => e.VisitationId);

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnType("DATETIME2")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Comments)
                    .IsRequired(false)
                    .IsUnicode()
                    .HasMaxLength(250);

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(e => e.PatientId)
                    .HasConstraintName("FK_Visitation_PatientId");

                entity.Property(e => e.DoctorId)
                    .IsRequired(false);

                entity.HasOne(e => e.Doctor)
                    .WithMany(d => d.Visitations)
                    .HasForeignKey(e => e.DoctorId);

            });

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(e => e.DiagnoseId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Comments)
                    .IsRequired(false)
                    .IsUnicode()
                    .HasMaxLength(250);

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(e => e.PatientId);

            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.MedicamentId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(50);

            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.MedicamentId });

                entity.HasOne(e => e.Medicament)
                    .WithMany(m => m.Prescriptions)
                    .HasForeignKey(e => e.MedicamentId);

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(e => e.PatientId);
            });  
               
        }

    }
}