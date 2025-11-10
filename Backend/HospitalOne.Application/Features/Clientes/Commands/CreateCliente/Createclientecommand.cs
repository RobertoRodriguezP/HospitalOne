using HospitalOne.Domain.Enums;
using MediatR;

namespace HospitalOne.Application.Features.Clientes.Commands.CreateCliente
{
    public record CreateClienteCommand : IRequest<int>
    {
        public string Nombres { get; init; } = string.Empty;
        public string Apellidos { get; init; } = string.Empty;
        public DateTime FechaNacimiento { get; init; }
        public Genero Genero { get; init; }
        public string? Telefono { get; init; }
        public string? Email { get; init; }
        public string? Direccion { get; init; }
        public string DocumentoIdentidad { get; init; } = string.Empty;
        public TipoDocumento TipoDocumento { get; init; }
    }
}