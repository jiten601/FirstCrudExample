using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FirstCrudExample.Models;

public partial class LunarDbContext : DbContext
{
    public LunarDbContext()
    {
    }

    public LunarDbContext(DbContextOptions<LunarDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Userlist> Userlists { get; set; }

    public virtual DbSet<Usertype> Usertypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("PK__Faculty__DBBF6FB15141C888");

            entity.ToTable("Faculty");

            entity.HasIndex(e => e.FacultyName, "UQ__Faculty__53353D25C4E9674C").IsUnique();

            entity.Property(e => e.FacultyId)
                .ValueGeneratedNever()
                .HasColumnName("facultyId");
            entity.Property(e => e.FacultyName)
                .HasMaxLength(50)
                .HasColumnName("facultyName");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StdId).HasName("PK__Student__BA08FEBB4FA7AD26");

            entity.ToTable("Student");

            entity.Property(e => e.StdId)
                .ValueGeneratedNever()
                .HasColumnName("stdId");
            entity.Property(e => e.FacultyId).HasColumnName("facultyId");
            entity.Property(e => e.JoinDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("joinDate");
            entity.Property(e => e.StdAddress)
                .HasMaxLength(50)
                .HasColumnName("stdAddress");
            entity.Property(e => e.StdName)
                .HasMaxLength(100)
                .HasColumnName("stdName");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Students)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__faculty__3B75D760");
        });

        modelBuilder.Entity<Userlist>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__USERLIST__CB9A1CFF506442B1");

            entity.ToTable("USERLIST");

            entity.HasIndex(e => e.PhoneNumber, "UQ__USERLIST__4849DA013F2E9EF7").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.LoginStatus).HasColumnName("loginStatus");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword).HasColumnName("userPassword");
            entity.Property(e => e.UserTypeId).HasColumnName("userTypeId");

            entity.HasOne(d => d.UserType).WithMany(p => p.Userlists)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USERLIST__userTy__59FA5E80");
        });

        modelBuilder.Entity<Usertype>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__USERNAME__F04DF13AE90FA02B");

            entity.ToTable("USERTYPE");

            entity.HasIndex(e => e.TypeName, "UQ__USERNAME__A20CDB58027F8CA3").IsUnique();

            entity.Property(e => e.TypeId)
                .ValueGeneratedNever()
                .HasColumnName("typeId");
            entity.Property(e => e.TypeName)
                .HasMaxLength(20)
                .HasColumnName("typeName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
