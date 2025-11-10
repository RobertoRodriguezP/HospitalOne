using HospitalOne.Domain.Enums;

namespace HospitalOne.Application.Features.Citas.Common
{
    public class CitaDto
    {
        public int CitaID { get; set; }
        public int ClienteID { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public int DoctorID { get; set; }
        public string DoctorNombre { get; set; } = string.Empty;
        public int ConsultorioID { get; set; }
        public string ConsultorioNumero { get; set; } = string.Empty;
        public int EspecialidadID { get; set; }
        public string EspecialidadNombre { get; set; } = string.Empty;
        public DateTime FechaCita { get; set; }
        public DateTime FechaFinEstimada { get; set; }
        public DateTime? FechaInicioReal { get; set; }
        public DateTime? FechaFinReal { get; set; }
        public int? TiempoDuracionMinutos { get; set; }
        public int? TiempoEsperaMinutos { get; set; }
        public TipoCita TipoCita { get; set; }
        public EstadoCita EstadoCita { get; set; }
        public string? Motivo { get; set; }
        public string? Diagnostico { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}