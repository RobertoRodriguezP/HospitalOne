using HospitalOne.Application.Common.Exceptions;
using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Features.Citas.Commands.UpdateEstadoCita
{
    public class UpdateEstadoCitaCommandHandler : IRequestHandler<UpdateEstadoCitaCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEstadoCitaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEstadoCitaCommand request, CancellationToken cancellationToken)
        {
            var cita = await _context.Citas
                .FirstOrDefaultAsync(c => c.CitaID == request.CitaID, cancellationToken);

            if (cita == null)
                throw new NotFoundException("Cita", request.CitaID);

            // Validar transiciones de estado
            ValidarTransicionEstado(cita.EstadoCita, request.NuevoEstado);

            // Actualizar estado
            cita.EstadoCita = request.NuevoEstado;

            // Actualizar fechas según el estado
            if (request.NuevoEstado == EstadoCita.EnCurso && request.FechaInicioReal.HasValue)
            {
                cita.FechaInicioReal = request.FechaInicioReal;
            }

            if (request.NuevoEstado == EstadoCita.Completada && request.FechaFinReal.HasValue)
            {
                cita.FechaFinReal = request.FechaFinReal;

                if (!string.IsNullOrEmpty(request.Diagnostico))
                    cita.Diagnostico = request.Diagnostico;

                if (!string.IsNullOrEmpty(request.Observaciones))
                    cita.Observaciones = request.Observaciones;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private void ValidarTransicionEstado(EstadoCita estadoActual, EstadoCita nuevoEstado)
        {
            // Validar que las transiciones de estado sean válidas
            var transicionesValidas = new Dictionary<EstadoCita, List<EstadoCita>>
            {
                { EstadoCita.Programada, new List<EstadoCita> { EstadoCita.EnCurso, EstadoCita.Cancelada, EstadoCita.NoAsistio } },
                { EstadoCita.EnCurso, new List<EstadoCita> { EstadoCita.Completada, EstadoCita.Cancelada } },
                { EstadoCita.Completada, new List<EstadoCita>() }, // No se puede cambiar desde completada
                { EstadoCita.Cancelada, new List<EstadoCita>() },   // No se puede cambiar desde cancelada
                { EstadoCita.NoAsistio, new List<EstadoCita>() }    // No se puede cambiar desde no asistió
            };

            if (!transicionesValidas[estadoActual].Contains(nuevoEstado))
            {
                throw new ValidationException(new[] {
                    new FluentValidation.Results.ValidationFailure("NuevoEstado",
                        $"No se puede cambiar de {estadoActual} a {nuevoEstado}.")
                });
            }
        }
    }
}