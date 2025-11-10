namespace HospitalOne.Domain.Enums
{
    /// <summary>
    /// Género del cliente
    /// </summary>
    public enum Genero
    {
        M, // Masculino
        F, // Femenino
        O  // Otro
    }

    /// <summary>
    /// Estado de disponibilidad del doctor
    /// </summary>
    public enum EstadoDisponibilidad
    {
        Disponible,    // Listo para ser asignado
        NoDisponible,  // Fuera de servicio (vacaciones, enfermo, etc.)
        EnConsulta,    // Atendiendo activamente a un paciente
        EnOcio         // Asignado a consultorio pero sin atender paciente
    }

    /// <summary>
    /// Estado del consultorio
    /// </summary>
    public enum EstadoConsultorio
    {
        Disponible,    // Listo para asignar doctor/paciente
        NoDisponible,  // Fuera de servicio (mantenimiento, limpieza)
        Citas,         // Consultorio activo atendiendo citas programadas
        Urgencias      // Consultorio activo atendiendo urgencias
    }

    /// <summary>
    /// Tipo de cita
    /// </summary>
    public enum TipoCita
    {
        Programada,
        Urgencia
    }

    /// <summary>
    /// Estado de la cita
    /// </summary>
    public enum EstadoCita
    {
        Programada,
        EnCurso,
        Completada,
        Cancelada,
        NoAsistio
    }

    /// <summary>
    /// Tipo de documento de identidad
    /// </summary>
    public enum TipoDocumento
    {
        Cédula,
        Pasaporte,
        RUC,
        CarnetExtranjeria
    }
}
