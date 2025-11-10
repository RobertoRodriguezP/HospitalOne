using HospitalOne.Domain.Enums;

namespace HospitalOne.Application.Features.Clientes.Common
{
    public class ClienteDto
    {
        public int ClienteID { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public Genero Genero { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string DocumentoIdentidad { get; set; } = string.Empty;
        public TipoDocumento TipoDocumento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
    }
}