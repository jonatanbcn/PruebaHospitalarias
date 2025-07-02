
using Core.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=HospitalJonatan;User Id=sa;Password=DB_Password;Encrypt=false;")
        .EnableSensitiveDataLogging()
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Nhc).HasName("PK__Paciente__C7DEDB1337829F7B");

            entity.ToTable("Paciente");

            entity.HasIndex(e => e.Dni, "UQ__Paciente__C035B8DD999F0568").IsUnique();

            entity.Property(e => e.Nhc).HasColumnName("NHC");
            entity.Property(e => e.Apellido1)
                .HasMaxLength(50)
                .HasColumnName("APELLIDO1");
            entity.Property(e => e.Apellido2).IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("APELLIDO2");
            entity.Property(e => e.Cip).IsRequired(false)
                .HasMaxLength(20)
                .HasColumnName("CIP");
            entity.Property(e => e.Dni)
                .HasMaxLength(9)
                .HasColumnName("DNI");
            entity.Property(e => e.Edad)
                .HasComputedColumnSql("(datediff(year,[FECHA_NACIMIENTO],getdate())-case when datepart(month,getdate())<datepart(month,[FECHA_NACIMIENTO]) OR datepart(month,getdate())=datepart(month,[FECHA_NACIMIENTO]) AND datepart(day,getdate())<datepart(day,[FECHA_NACIMIENTO]) then (1) else (0) end)", false)
                .HasColumnName("EDAD");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("FECHA_NACIMIENTO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Observaciones).IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("OBSERVACIONES");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SEXO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
