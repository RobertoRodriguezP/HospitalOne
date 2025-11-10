using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Application.Features.Citas.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Features.Citas.Queries.GetCitasByCliente
{
    public class GetCitasByClienteQueryHandler : IRequestHandler<GetCitasByClienteQuery, List<CitaDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCitasByClienteQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CitaDto>> Handle(GetCitasByClienteQuery request, CancellationToken cancellationToken)
        {
            return await _context.Citas
                .AsNoTracking()
                .Include(c => c.Cliente)
                .Include(c => c.Doctor)
                .Include(c => c.Consultorio)
                .Include(c => c.Especialidad)
                .Where(c => c.ClienteID == request.ClienteId)
                .OrderByDescending(c => c.FechaCita)
                .Select(c => new CitaDto
                {
                    CitaID = c.CitaID,
                    ClienteID = c.ClienteID,
                    ClienteNombre = c.Cliente.NombreCompleto,
                    DoctorID = c.DoctorID,
                    DoctorNombre = c.Doctor.NombreCompleto,
                    ConsultorioID = c.ConsultorioID,
                    ConsultorioNumero = c.Consultorio.NumeroConsultorio,
                    EspecialidadID = c.EspecialidadID,
                    EspecialidadNombre = c.Especialidad.NombreEspecialidad,
                    FechaCita = c.FechaCita,
                    FechaFinEstimada = c.FechaFinEstimada,
                    FechaInicioReal = c.FechaInicioReal,
                    FechaFinReal = c.FechaFinReal,
                    TiempoDuracionMinutos = c.TiempoDuracionMinutos,
                    TiempoEsperaMinutos = c.TiempoEsperaMinutos,
                    TipoCita = c.TipoCita,
                    EstadoCita = c.EstadoCita,
                    Motivo = c.Motivo,
                    Diagnostico = c.Diagnostico,
                    Observaciones = c.Observaciones,
                    FechaRegistro = c.FechaRegistro
                })
                .ToListAsync(cancellationToken);
        }
    }
}