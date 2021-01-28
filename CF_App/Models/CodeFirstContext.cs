using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CF_App.Models
{
    // Add-Migartion AddedPatientAndPrescriptionAnd...DoctorTables //komenda do migracji w Konsoli menedzera pakietów !!!

    public class CodeFirstContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicament { get; set; }

        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient).HasName("Patient_PK");

                entity.Property(e => e.IdPatient).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.BirthDate).IsRequired();
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription).HasName("Prescription_PK");

                entity.Property(e => e.IdPrescription).ValueGeneratedNever();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.IdPatient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Prescription_Patient");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.IdDoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Prescription_Doctor");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("Doctor_PK");

                entity.Property(e => e.IdDoctor).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament).HasName("Medicament_PK");

                entity.Property(e => e.IdMedicament).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {
                entity.HasKey(e => new { e.IdMedicament, e.IdPrescription}).HasName("PrescriptionMedicament_PK");

                entity.Property(e => e.IdMedicament).ValueGeneratedNever();
                entity.Property(e => e.IdPrescription).ValueGeneratedNever();
                entity.Property(e => e.Dose).IsRequired();
                entity.Property(e => e.Details).HasMaxLength(100).IsRequired();

                entity.HasOne(d => d.Medicament)
                    .WithMany(p => p.PrescriptionMedicaments)
                    .HasForeignKey(d => d.IdMedicament)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PrescriptionMedicament_Medicament");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionMedicaments)
                    .HasForeignKey(d => d.IdPrescription)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PrescriptionMedicament_Prescription");
            });
        }
    }


    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    IdPatient = 1,
                    FirstName = "Aliaksandr",
                    LastName = "Shyleika"
                },
                new Patient
                {
                    IdPatient = 2,
                    FirstName = "Bob",
                    LastName = "Kelso"
                }
            );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    IdDoctor = 1,
                    FirstName = "John",
                    LastName = "Doryan",
                    Email = "jd@scrubs.com"
                });

            modelBuilder.Entity<Medicament>().HasData(
                new Medicament
                {
                    IdMedicament = 1,
                    Name = "some medicament name",
                    Description = "some fancy description",
                    Type = "Drugs"
                });

            modelBuilder.Entity<Prescription>().HasData(
                new Prescription
                {
                    IdPrescription = 1,
                    Date = new DateTime(2020, 5, 1),
                    DueDate = new DateTime(2020, 7, 1),
                    IdDoctor = 1,
                    IdPatient = 2
                });

            modelBuilder.Entity<PrescriptionMedicament>().HasData(
                new PrescriptionMedicament
                {
                    IdMedicament = 1,
                    IdPrescription = 1,
                    Dose = 999,
                    Details = "some interesting details"
                });
        }
    }
}
