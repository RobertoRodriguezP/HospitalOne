using HospitalOne.Domain.Enums;
using MediatR;

namespace HospitalOne.Application.Features.Citas.Commands.CreateCita
{
    public record CreateCitaCommand : IRequest<int>
    {
        public int ClienteID { get; init; }
        public int DoctorID { get; init; }
        public int ConsultorioID { get; init; }
        public int EspecialidadID { get; init; }
        public DateTime FechaCita { get; init; }
        public int DuracionEstimadaMinutos { get; init; }
        public TipoCita TipoCita { get; init; } = TipoCita.Programada;
        public string? Motivo { get; init; }
        public string? UsuarioRegistro { get; init; }
    }
}