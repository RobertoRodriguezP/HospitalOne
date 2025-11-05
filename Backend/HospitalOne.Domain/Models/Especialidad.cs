using HospitalOne.Domain.Enums;


namespace HospitalOne.Domain.Models
{
    public class Especialidad
    {
        public int EspecialidadID { get; set; }

        public string NombreEspecialidad { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        /// <summary>
        /// Tiempo promedio de consulta en minutos
        /// </summary>
        public int TiempoPromedioConsulta { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Navegación
        public virtual ICollection<Doctor> Doctores { get; set; } = new List<Doctor>();
        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
