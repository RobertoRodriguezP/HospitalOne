using HospitalOne.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Cliente> Clientes { get; }
    DbSet<Consultorio> Consultorios { get; }
    DbSet<Especialidad> Especialidades { get; }
    DbSet<Doctor> Doctores { get; }
    DbSet<Cita> Citas { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}