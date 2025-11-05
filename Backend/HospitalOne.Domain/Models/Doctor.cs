using HospitalOne.Domain.Enums;


namespace HospitalOne.Domain.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string DocumentoIdentidad { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public string NumeroLicencia { get; set; } = string.Empty;

        public int EspecialidadID { get; set; }

        public EstadoDisponibilidad EstadoDisponibilidad { get; set; } = EstadoDisponibilidad.Disponible;

        public DateTime FechaContratacion { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Propiedades calculadas
        public string NombreCompleto => $"{Nombres} {Apellidos}";

        public int AñosServicio
        {
            get
            {
                return DateTime.Today.Year - FechaContratacion.Year;
            }
        }

        // Navegación
        public virtual Especialidad Especialidad { get; set; } = null!;
        public virtual ICollection<Consultorio> Consultorios { get; set; } = new List<Consultorio>();
        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
