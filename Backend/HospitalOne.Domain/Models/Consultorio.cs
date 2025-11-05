using HospitalOne.Domain.Enums;

namespace HospitalOne.Domain.Models
{
    public class Consultorio
    {
        public int ConsultorioID { get; set; }

        public string NumeroConsultorio { get; set; } = string.Empty;

        public int Piso { get; set; }

        public string? EdificioAla { get; set; }

        public string? TipoConsultorio { get; set; }

        public EstadoConsultorio EstadoConsultorio { get; set; } = EstadoConsultorio.Disponible;

        public int? DoctorAsignadoID { get; set; }

        public DateTime? FechaAsignacionDoctor { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime UltimaActualizacion { get; set; } = DateTime.Now;

        // Propiedades calculadas
        public string UbicacionCompleta => $"Piso {Piso} - {EdificioAla ?? "Sin ala"} - #{NumeroConsultorio}";

        public bool TieneDoctorAsignado => DoctorAsignadoID.HasValue;

        // Navegación
        public virtual Doctor? DoctorAsignado { get; set; }
        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
