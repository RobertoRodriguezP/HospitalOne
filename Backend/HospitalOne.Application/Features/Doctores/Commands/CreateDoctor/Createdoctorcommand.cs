using HospitalOne.Domain.Enums;
using MediatR;

namespace HospitalOne.Application.Features.Doctores.Commands.CreateDoctor
{
    public record CreateDoctorCommand : IRequest<int>
    {
        public string Nombres { get; init; } = string.Empty;
        public string Apellidos { get; init; } = string.Empty;
        public string DocumentoIdentidad { get; init; } = string.Empty;
        public string? Telefono { get; init; }
        public string? Email { get; init; }
        public string NumeroLicencia { get; init; } = string.Empty;
        public int EspecialidadID { get; init; }
        public DateTime FechaContratacion { get; init; }
    }
}