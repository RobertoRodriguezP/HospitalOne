using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Application.Features.Doctores.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Features.Doctores.Queries.GetDoctores
{
    public class GetDoctoresQueryHandler : IRequestHandler<GetDoctoresQuery, List<DoctorDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetDoctoresQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DoctorDto>> Handle(GetDoctoresQuery request, CancellationToken cancellationToken)
        {
            return await _context.Doctores
                .AsNoTracking()
                .Include(d => d.Especialidad)
                .Where(d => d.Activo)
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