using HospitalOne.Domain.Enums;

namespace HospitalOne.Application.Features.Consultorios.Common
{
    public class ConsultorioDto
    {
        public int ConsultorioID { get; set; }
        public string NumeroConsultorio { get; set; } = string.Empty;
        public int Piso { get; set; }
        public string? EdificioAla { get; set; }
        public string UbicacionCompleta { get; set; } = string.Empty;
        public string? TipoConsultorio { get; set; }
        public EstadoConsultorio EstadoConsultorio { get; set; }
        public int? DoctorAsignadoID { get; set; }
        public string? DoctorAsignadoNombre { get; set; }
        public DateTime? FechaAsignacionDoctor { get; set; }
        public bool Activo { get; set; }
    }
}