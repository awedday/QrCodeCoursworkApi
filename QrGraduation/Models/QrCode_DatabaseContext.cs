using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QrGraduation.Models
{
    public partial class QrCode_DatabaseContext : DbContext
    {
        public QrCode_DatabaseContext()
        {
        }

        public QrCode_DatabaseContext(DbContextOptions<QrCode_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeePosition> EmployeePositions { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Information> Information { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Qr> Qrs { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee);

                entity.ToTable("Employee");

                entity.HasIndex(e => e.PhoneEmployee, "UQ_Unique_Phone_Employee")
                    .IsUnique();

                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");

                entity.Property(e => e.FirstNameEmployee)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("First_Name_Employee");

                entity.Property(e => e.MailEmployee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Mail_Employee");

                entity.Property(e => e.MiddleNameEmployee)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Middle_Name_Employee");

                entity.Property(e => e.PasswordEmployee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Password_Employee");

                entity.Property(e => e.PhoneEmployee)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Employee");

                entity.Property(e => e.SecondNameEmployee)
                    .HasMaxLength(103)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name_Employee");
            });

            modelBuilder.Entity<EmployeePosition>(entity =>
            {
                entity.HasKey(e => e.IdEmployeePosition);

                entity.ToTable("Employee_Position");

                entity.Property(e => e.IdEmployeePosition).HasColumnName("ID_Employee_Position");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.PositionId).HasColumnName("Position_ID");

               
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.IdHistory);

                entity.ToTable("History");

                entity.Property(e => e.IdHistory).HasColumnName("ID_History");

                entity.Property(e => e.DateFinishHistory)
                    .HasMaxLength(19)
                    .IsUnicode(false)
                    .HasColumnName("DateFinish_History");

                entity.Property(e => e.DateStartHistory)
                    .HasMaxLength(19)
                    .IsUnicode(false)
                    .HasColumnName("DateStart_History");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

               
            });

            modelBuilder.Entity<Information>(entity =>
            {
                entity.HasKey(e => e.IdInformation);

                entity.Property(e => e.IdInformation).HasColumnName("ID_Information");

                entity.Property(e => e.AndroidInformation)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("Android_Information");

                entity.Property(e => e.DistanceInformation).HasColumnName("Distance_Information");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.LocationInformation)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("Location_Information");

               
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.IdPosition);

                entity.ToTable("Position");

                entity.HasIndex(e => e.NamePosition, "UQ_Name_Position")
                    .IsUnique();

                entity.Property(e => e.IdPosition).HasColumnName("ID_Position");

                entity.Property(e => e.NamePosition)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Name_Position");
            });

            modelBuilder.Entity<Qr>(entity =>
            {
                entity.HasKey(e => e.IdQr);

                entity.ToTable("QR");

                entity.Property(e => e.IdQr).HasColumnName("ID_QR");

                entity.Property(e => e.TextQr)
                    .HasMaxLength(180)
                    .IsUnicode(false)
                    .HasColumnName("Text_QR");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
