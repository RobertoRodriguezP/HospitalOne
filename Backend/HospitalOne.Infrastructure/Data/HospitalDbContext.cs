using HospitalOne.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalOne.Infrastructure.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar todas las configuraciones del ensamblado
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalDbContext).Assembly);

            // O aplicarlas manualmente si prefieres:
            // modelBuilder.ApplyConfiguration(new EspecialidadConfiguration());
            // modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            // modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            // modelBuilder.ApplyConfiguration(new ConsultorioConfiguration());
            // modelBuilder.ApplyConfiguration(new CitaConfiguration());
        }
    }
}
