using HospitalOne.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalOne.Domain.Models
{
    public class Cita
    {
        public int CitaID { get; set; }

        public int ClienteID { get; set; }

        public int DoctorID { get; set; }

        public int ConsultorioID { get; set; }

        public int EspecialidadID { get; set; }

        public DateTime FechaCita { get; set; }

        public DateTime FechaFinEstimada { get; set; }

        public DateTime? FechaInicioReal { get; set; }

        public DateTime? FechaFinReal { get; set; }

        /// <summary>
        /// Duración en minutos (calculada automáticamente en BD, pero puede ser asignada manualmente)
        /// </summary>
        public int? TiempoDuracionMinutos
        {
            get
            {
                if (FechaInicioReal.HasValue && FechaFinReal.HasValue)
                {
                    return (int)(FechaFinReal.Value - FechaInicioReal.Value).TotalMinutes;
                }
                return null;
            }
        }

        public TipoCita TipoCita { get; set; } = TipoCita.Programada;

        public EstadoCita EstadoCita { get; set; } = EstadoCita.Programada;

        public string? Motivo { get; set; }

        public string? Diagnostico { get; set; }

        public string? Observaciones { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public string? UsuarioRegistro { get; set; }

        // Propiedades calculadas
        public int? TiempoEsperaMinutos
        {
            get
            {
                if (FechaInicioReal.HasValue)
                {
                    return (int)(FechaInicioReal.Value - FechaCita).TotalMinutes;
                }
                return null;
            }
        }

        public bool EstaCompletada => EstadoCita == EstadoCita.Completada;

        public bool EstaEnCurso => EstadoCita == EstadoCita.EnCurso;

        public bool EstaPendiente => EstadoCita == EstadoCita.Programada;

        // Navegación
        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Consultorio Consultorio { get; set; } = null!;
        public virtual Especialidad Especialidad { get; set; } = null!;
    }
}
