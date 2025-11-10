using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Application.Features.Especialidades.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Features.Especialidades.Queries.GetEspecialidades
{
    public class GetEspecialidadesQueryHandler : IRequestHandler<GetEspecialidadesQuery, List<EspecialidadDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetEspecialidadesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EspecialidadDto>> Handle(GetEspecialidadesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Especialidades
                .AsNoTracking()
                .Where(e => e.Activo)
                .OrderBy(e => e.NombreEspecialidad)
                .Select(e => new EspecialidadDto
                {
                    EspecialidadID = e.EspecialidadID,
                    NombreEspecialidad = e.NombreEspecialidad,
                    Descripcion = e.Descripcion,
                    TiempoPromedioConsulta = e.TiempoPromedioConsulta,
                    Activo = e.Activo,
                    CantidadDoctores = e.Doctores.Count(d => d.Activo)
                })
                .ToListAsync(cancellationToken);
        }
    }
}