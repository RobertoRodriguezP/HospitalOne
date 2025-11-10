using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Application.Features.Doctores.Common;
using HospitalOne.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Features.Doctores.Queries.GetDoctoresDisponiblesPorEspecialidad
{
    public class GetDoctoresDisponiblesPorEspecialidadQueryHandler
        : IRequestHandler<GetDoctoresDisponiblesPorEspecialidadQuery, List<DoctorDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetDoctoresDisponiblesPorEspecialidadQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DoctorDto>> Handle(
            GetDoctoresDisponiblesPorEspecialidadQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Doctores
                .AsNoTracking()
                .Include(d => d.Especialidad)
                .Where(d => d.EspecialidadID == request.EspecialidadId
                    && d.Activo
                    && d.EstadoDisponibilidad == EstadoDisponibilidad.Disponible)
                .OrderBy(d => d.Apellidos)
                .ThenBy(d => d.Nombres)
                .Select(d => new DoctorDto
                {
                    DoctorID = d.DoctorID,
                    Nombres = d.Nombres,
                    Apellidos = d.Apellidos,
                    NombreCompleto = d.NombreCompleto,
                    DocumentoIdentidad = d.DocumentoIdentidad,
                    Telefono = d.Telefono,
                    Email = d.Email,
                    NumeroLicencia = d.NumeroLicencia,
                    EspecialidadID = d.EspecialidadID,
                    EspecialidadNombre = d.Especialidad.NombreEspecialidad,
                    EstadoDisponibilidad = d.EstadoDisponibilidad,
                    FechaContratacion = d.FechaContratacion,
                    AñosServicio = d.AñosServicio,
                    Activo = d.Activo,
                    CantidadCitas = d.Citas.Count
                })
                .ToListAsync(cancellationToken);
        }
    }
}