using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Application.Features.Consultorios.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Features.Consultorios.Queries.GetConsultorios
{
    public class GetConsultoriosQueryHandler : IRequestHandler<GetConsultoriosQuery, List<ConsultorioDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetConsultoriosQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ConsultorioDto>> Handle(GetConsultoriosQuery request, CancellationToken cancellationToken)
        {
            return await _context.Consultorios
                .AsNoTracking()
                .Include(c => c.DoctorAsignado)
                .Where(c => c.Activo)
                .OrderBy(c => c.Piso)
                .ThenBy(c => c.NumeroConsultorio)
                .Select(c => new ConsultorioDto
                {
                    ConsultorioID = c.ConsultorioID,
                    NumeroConsultorio = c.NumeroConsultorio,
                    Piso = c.Piso,
                    EdificioAla = c.EdificioAla,
                    UbicacionCompleta = c.UbicacionCompleta,
                    TipoConsultorio = c.TipoConsultorio,
                    EstadoConsultorio = c.EstadoConsultorio,
                    DoctorAsignadoID = c.DoctorAsignadoID,
                    DoctorAsignadoNombre = c.DoctorAsignado != null ? c.DoctorAsignado.NombreCompleto : null,
                    FechaAsignacionDoctor = c.FechaAsignacionDoctor,
                    Activo = c.Activo
                })
                .ToListAsync(cancellationToken);
        }
    }
}