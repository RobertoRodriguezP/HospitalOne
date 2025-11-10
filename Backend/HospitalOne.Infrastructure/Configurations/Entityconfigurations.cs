using HospitalOne.Domain.Enums;
using HospitalOne.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace HospitalOne.Infrastructure.Configurations
{
    public class EspecialidadConfiguration : IEntityTypeConfiguration<Especialidad>
    {
        public void Configure(EntityTypeBuilder<Especialidad> builder)
        {
            builder.ToTable("Especialidades", "HospitalOne");

            builder.HasKey(e => e.EspecialidadID);

            builder.Property(e => e.NombreEspecialidad)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(e => e.NombreEspecialidad).IsUnique();

            builder.Property(e => e.Descripcion)
                .HasMaxLength(500);

            builder.Property(e => e.TiempoPromedioConsulta)
                .IsRequired();

            builder.Property(e => e.Activo)
                .HasDefaultValue(true);

            builder.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");
        }
    }

    /// <summary>
    /// Configuración de Entity Framework para la entidad Cliente
    /// </summary>
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes", "HospitalOne");

            builder.HasKey(c => c.ClienteID);

            builder.Property(c => c.Nombres)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Apellidos)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.FechaNacimiento)
                .IsRequired();

            builder.Property(c => c.Genero)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(1);

            builder.Property(c => c.Telefono)
                .HasMaxLength(20);

            builder.Property(c => c.Email)
                .HasMaxLength(100);

            builder.Property(c => c.Direccion)
                .HasMaxLength(200);

            builder.Property(c => c.DocumentoIdentidad)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(c => c.DocumentoIdentidad).IsUnique();

            builder.Property(c => c.TipoDocumento)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(c => c.FechaRegistro)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.Activo)
                .HasDefaultValue(true);

            // Ignorar propiedades calculadas
            builder.Ignore(c => c.NombreCompleto);
            builder.Ignore(c => c.Edad);
        }
    }

    /// <summary>
    /// Configuración de Entity Framework para la entidad Doctor
    /// </summary>
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctores", "HospitalOne");

            builder.HasKey(d => d.DoctorID);

            builder.Property(d => d.Nombres)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Apellidos)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.DocumentoIdentidad)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(d => d.DocumentoIdentidad).IsUnique();

            builder.Property(d => d.Telefono)
                .HasMaxLength(20);

            builder.Property(d => d.Email)
                .HasMaxLength(100);

            builder.Property(d => d.NumeroLicencia)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(d => d.NumeroLicencia).IsUnique();

            builder.Property(d => d.EstadoDisponibilidad)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValue(EstadoDisponibilidad.Disponible);

            builder.Property(d => d.FechaContratacion)
                .IsRequired();

            builder.Property(d => d.Activo)
                .HasDefaultValue(true);

            builder.Property(d => d.FechaRegistro)
                .HasDefaultValueSql("GETDATE()");

            // Relaciones
            builder.HasOne(d => d.Especialidad)
                .WithMany(e => e.Doctores)
                .HasForeignKey(d => d.EspecialidadID)
                .OnDelete(DeleteBehavior.Restrict);

            // Ignorar propiedades calculadas
            builder.Ignore(d => d.NombreCompleto);
            builder.Ignore(d => d.AñosServicio);
        }
    }

    /// <summary>
    /// Configuración de Entity Framework para la entidad Consultorio
    /// </summary>
    public class ConsultorioConfiguration : IEntityTypeConfiguration<Consultorio>
    {
        public void Configure(EntityTypeBuilder<Consultorio> builder)
        {
            builder.ToTable("Consultorios", "HospitalOne");

            builder.HasKey(c => c.ConsultorioID);

            builder.Property(c => c.NumeroConsultorio)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasIndex(c => c.NumeroConsultorio).IsUnique();

            builder.Property(c => c.Piso)
                .IsRequired();

            builder.Property(c => c.EdificioAla)
                .HasMaxLength(50);

            builder.Property(c => c.TipoConsultorio)
                .HasMaxLength(50);

            builder.Property(c => c.EstadoConsultorio)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValue(EstadoConsultorio.Disponible);

            builder.Property(c => c.Activo)
                .HasDefaultValue(true);

            builder.Property(c => c.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UltimaActualizacion)
                .HasDefaultValueSql("GETDATE()");

            // Relaciones
            builder.HasOne(c => c.DoctorAsignado)
                .WithMany(d => d.Consultorios)
                .HasForeignKey(c => c.DoctorAsignadoID)
                .OnDelete(DeleteBehavior.SetNull);

            // Ignorar propiedades calculadas
            builder.Ignore(c => c.UbicacionCompleta);
            builder.Ignore(c => c.TieneDoctorAsignado);
        }
    }

    /// <summary>
    /// Configuración de Entity Framework para la entidad Cita
    /// </summary>
    public class CitaConfiguration : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ToTable("Citas", "HospitalOne");

            builder.HasKey(c => c.CitaID);

            builder.Property(c => c.FechaCita)
                .IsRequired();

            builder.Property(c => c.FechaFinEstimada)
                .IsRequired();

            builder.Property(c => c.TipoCita)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValue(TipoCita.Programada);

            builder.Property(c => c.EstadoCita)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValue(EstadoCita.Programada);

            builder.Property(c => c.Motivo)
                .HasMaxLength(500);

            builder.Property(c => c.Diagnostico)
                .HasMaxLength(1000);

            builder.Property(c => c.Observaciones)
                .HasMaxLength(1000);

            builder.Property(c => c.FechaRegistro)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UsuarioRegistro)
                .HasMaxLength(100);

            // Relaciones
            builder.HasOne(c => c.Cliente)
                .WithMany(cl => cl.Citas)
                .HasForeignKey(c => c.ClienteID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Doctor)
                .WithMany(d => d.Citas)
                .HasForeignKey(c => c.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Consultorio)
                .WithMany(co => co.Citas)
                .HasForeignKey(c => c.ConsultorioID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Especialidad)
                .WithMany(e => e.Citas)
                .HasForeignKey(c => c.EspecialidadID)
                .OnDelete(DeleteBehavior.Restrict);

            // Ignorar propiedades calculadas
            builder.Ignore(c => c.TiempoDuracionMinutos);
            builder.Ignore(c => c.TiempoEsperaMinutos);
            builder.Ignore(c => c.EstaCompletada);
            builder.Ignore(c => c.EstaEnCurso);
            builder.Ignore(c => c.EstaPendiente);
        }
    }
}
