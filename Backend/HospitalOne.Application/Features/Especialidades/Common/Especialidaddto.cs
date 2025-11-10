namespace HospitalOne.Application.Features.Especialidades.Common
{
    public class EspecialidadDto
    {
        public int EspecialidadID { get; set; }
        public string NombreEspecialidad { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int TiempoPromedioConsulta { get; set; }
        public bool Activo { get; set; }
        public int CantidadDoctores { get; set; }
    }
}