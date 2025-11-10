using MediatR;

namespace HospitalOne.Application.Features.Consultorios.Commands.CreateConsultorio
{
    public record CreateConsultorioCommand : IRequest<int>
    {
        public string NumeroConsultorio { get; init; } = string.Empty;
        public int Piso { get; init; }
        public string? EdificioAla { get; init; }
        public string? TipoConsultorio { get; init; }
    }
}