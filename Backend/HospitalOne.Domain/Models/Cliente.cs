using HospitalOne.Domain.Enums;

namespace HospitalOne.Domain.Models
{
    public class Cliente
    {
        public int ClienteID { get; set; }

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        public Genero Genero { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public string? Direccion { get; set; }

        public string DocumentoIdentidad { get; set; } = string.Empty;

        public TipoDocumento TipoDocumento { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;

        // Propiedades calculadas
        public string NombreCompleto => $"{Nombres} {Apellidos}";

        public int Edad
        {
            get
            {
                var hoy = DateTime.Today;
                var edad = hoy.Year - FechaNacimiento.Year;
                if (FechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
                return edad;
            }
        }

        // Navegación
        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
