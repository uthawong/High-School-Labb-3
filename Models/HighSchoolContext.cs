using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace High_School_Labb_3.Models;

public partial class HighSchoolContext : DbContext
{
    public HighSchoolContext()
    {
    }

    public HighSchoolContext(DbContextOptions<HighSchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocaldb;DataBase=HighSchool;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.ToTable("Class");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.Class1)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Class");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkFacultyId).HasColumnName("FK_FacultyID");

            entity.HasOne(d => d.FkFaculty).WithMany(p => p.Courses)
                .HasForeignKey(d => d.FkFacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Courses_Faculty");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.DateOfGrade).HasColumnType("date");
            entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseID");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");
            entity.Property(e => e.GradeInfo).HasMaxLength(10);

            entity.HasOne(d => d.FkCourse).WithMany()
                .HasForeignKey(d => d.FkCourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollments_Courses");

            entity.HasOne(d => d.FkStudent).WithMany()
                .HasForeignKey(d => d.FkStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollments_Student");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.ToTable("Faculty");

            entity.Property(e => e.FacultyId).HasColumnName("FacultyID");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.FkRoleId).HasColumnName("FK_RoleID");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.FkRole).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.FkRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Faculty_Roles");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Role1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.FkClassId).HasColumnName("FK_ClassID");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Major)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.FkClass).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Class");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
