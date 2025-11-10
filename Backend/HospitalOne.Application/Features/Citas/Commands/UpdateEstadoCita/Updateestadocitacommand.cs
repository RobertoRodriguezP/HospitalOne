using HospitalOne.Domain.Enums;
using MediatR;

namespace HospitalOne.Application.Features.Citas.Commands.UpdateEstadoCita
{
    public record UpdateEstadoCitaCommand : IRequest<Unit>
    {
        public int CitaID { get; init; }
        public EstadoCita NuevoEstado { get; init; }
        public DateTime? FechaInicioReal { get; init; }
        public DateTime? FechaFinReal { get; init; }
        public string? Diagnostico { get; init; }
        public string? Observaciones { get; init; }
    }
}