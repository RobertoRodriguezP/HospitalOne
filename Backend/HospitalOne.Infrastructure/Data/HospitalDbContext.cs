using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalOne.Infrastructure.Data
{
    public class HospitalDbContext : DbContext, IApplicationDbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<Cita> Citas { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalDbContext).Assembly);
        }
    }
}
