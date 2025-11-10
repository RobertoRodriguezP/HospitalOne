using HospitalOne.Domain.Enums;

namespace HospitalOne.Application.Features.Doctores.Common
{
    public class DoctorDto
    {
        public int DoctorID { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string DocumentoIdentidad { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string NumeroLicencia { get; set; } = string.Empty;
        public int EspecialidadID { get; set; }
        public string EspecialidadNombre { get; set; } = string.Empty;
        public EstadoDisponibilidad EstadoDisponibilidad { get; set; }
        public DateTime FechaContratacion { get; set; }
        public int AñosServicio { get; set; }
        public bool Activo { get; set; }
        public int CantidadCitas { get; set; }
    }
}