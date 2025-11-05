-- ============================================
-- Script 8: Pruebas del Sistema
-- Base de Datos: Hospital
-- Esquema: HospitalOne
-- ============================================



PRINT '============================================';
PRINT 'INICIANDO PRUEBAS DEL SISTEMA HOSPITALARIO';
PRINT '============================================';


-- ============================================
-- PRUEBA 1: Asignar Doctores a Consultorios
-- ============================================
PRINT '';
PRINT '--- PRUEBA 1: Asignar Doctores a Consultorios ---';


-- Asignar Doctor Carlos Méndez (Medicina General) al Consultorio C-101 para Citas
EXEC HospitalOne.SP_AsignarDoctorConsultorio 
    @DoctorID = 1, 
    @ConsultorioID = 1, 
    @TipoServicio = 'Citas';


-- Asignar Doctora Ana María Rodríguez (Cardiología) al Consultorio C-102
EXEC HospitalOne.SP_AsignarDoctorConsultorio 
    @DoctorID = 2, 
    @ConsultorioID = 2, 
    @TipoServicio = 'Citas';


-- Asignar Doctor José González (Pediatría) al Consultorio C-103 para Urgencias
EXEC HospitalOne.SP_AsignarDoctorConsultorio 
    @DoctorID = 3, 
    @ConsultorioID = 3, 
    @TipoServicio = 'Urgencias';


-- Verificar estado de doctores y consultorios
PRINT 'Estado de Doctores después de asignación:';
SELECT DoctorID, Nombres + ' ' + Apellidos AS NombreDoctor, EstadoDisponibilidad 
FROM HospitalOne.Doctores 
WHERE DoctorID IN (1, 2, 3);


PRINT 'Estado de Consultorios:';
SELECT * FROM HospitalOne.VW_ConsultoriosActivos 
WHERE ConsultorioID IN (1, 2, 3);


-- ============================================
-- PRUEBA 2: Crear Citas
-- ============================================
PRINT '';
PRINT '--- PRUEBA 2: Crear Citas ---';


DECLARE @CitaID1 INT, @CitaID2 INT, @CitaID3 INT;

-- Cita 1: Juan Pérez con Dr. Carlos Méndez (Medicina General)
EXEC HospitalOne.SP_CrearCita
    @ClienteID = 1,
    @DoctorID = 1,
    @ConsultorioID = 1,
    @EspecialidadID = 1,
    @FechaCita = '2024-11-05 09:00:00',
    @TipoCita = 'Programada',
    @Motivo = 'Chequeo médico general',
    @CitaID = @CitaID1 OUTPUT;
    
PRINT 'Cita creada con ID: ' + CAST(@CitaID1 AS NVARCHAR(10));


-- Cita 2: Laura Martínez con Dra. Ana María Rodríguez (Cardiología)
DECLARE @CitaID2 INT;
EXEC HospitalOne.SP_CrearCita
    @ClienteID = 2,
    @DoctorID = 2,
    @ConsultorioID = 2,
    @EspecialidadID = 2,
    @FechaCita = '2024-11-05 10:00:00',
    @TipoCita = 'Programada',
    @Motivo = 'Control cardiológico',
    @CitaID = @CitaID2 OUTPUT;
    
PRINT 'Cita creada con ID: ' + CAST(@CitaID2 AS NVARCHAR(10));


-- Cita 3: Sofía Hernández con Dr. José González (Pediatría) - Urgencia
DECLARE @CitaID3 INT;
EXEC HospitalOne.SP_CrearCita
    @ClienteID = 4,
    @DoctorID = 3,
    @ConsultorioID = 3,
    @EspecialidadID = 3,
    @FechaCita = '2024-11-05 10:30:00',
    @TipoCita = 'Urgencia',
    @Motivo = 'Fiebre alta',
    @CitaID = @CitaID3 OUTPUT;
    
PRINT 'Cita creada con ID: ' + CAST(@CitaID3 AS NVARCHAR(10));


-- Verificar citas creadas
PRINT 'Citas programadas:';
SELECT CitaID, ClienteID, DoctorID, ConsultorioID, FechaCita, EstadoCita, TipoCita, Motivo
FROM HospitalOne.Citas
WHERE CitaID IN (1, 2, 3);


-- ============================================
-- PRUEBA 3: Iniciar y Completar Citas
-- ============================================
PRINT '';
PRINT '--- PRUEBA 3: Iniciar y Completar Citas ---';


-- Iniciar Cita 1
EXEC HospitalOne.SP_IniciarCita @CitaID = 1;


-- Verificar cambio de estado del doctor
PRINT 'Estado del Dr. Carlos Méndez después de iniciar cita:';
SELECT DoctorID, Nombres + ' ' + Apellidos AS NombreDoctor, EstadoDisponibilidad 
FROM HospitalOne.Doctores 
WHERE DoctorID = 1;


-- Esperar un momento simulado (en producción la cita estaría en curso)
WAITFOR DELAY '00:00:02';


-- Completar Cita 1
EXEC HospitalOne.SP_CompletarCita 
    @CitaID = 1,
    @Diagnostico = 'Paciente en buen estado de salud. Presión arterial normal. Se recomienda continuar con hábitos saludables.',
    @Observaciones = 'Control en 6 meses';


-- Verificar estado del doctor después de completar
PRINT 'Estado del Dr. Carlos Méndez después de completar cita:';
SELECT DoctorID, Nombres + ' ' + Apellidos AS NombreDoctor, EstadoDisponibilidad 
FROM HospitalOne.Doctores 
WHERE DoctorID = 1;


-- Iniciar y completar Cita 2
EXEC HospitalOne.SP_IniciarCita @CitaID = 2;


WAITFOR DELAY '00:00:02';


EXEC HospitalOne.SP_CompletarCita 
    @CitaID = 2,
    @Diagnostico = 'Ritmo cardíaco estable. ECG normal. Continuar con medicación actual.',
    @Observaciones = 'Próximo control en 3 meses';


-- ============================================
-- PRUEBA 4: Cancelar una Cita
-- ============================================
PRINT '';
PRINT '--- PRUEBA 4: Cancelar una Cita ---';


EXEC HospitalOne.SP_CancelarCita 
    @CitaID = 3,
    @Motivo = 'Paciente canceló por motivos personales';


-- Verificar cita cancelada
PRINT 'Estado de la cita cancelada:';
SELECT CitaID, EstadoCita, Observaciones 
FROM HospitalOne.Citas 
WHERE CitaID = 3;


-- ============================================
-- PRUEBA 5: Verificar Históricos
-- ============================================
PRINT '';
PRINT '--- PRUEBA 5: Verificar Históricos ---';


-- Histórico de Citas
PRINT 'Histórico de Citas Completadas/Canceladas:';
SELECT * FROM HospitalOne.Citas_Historico;


-- Histórico de Consultorios
PRINT 'Histórico de Cambios en Consultorios:';
SELECT * FROM HospitalOne.Consultorios_Historico;


-- ============================================
-- PRUEBA 6: Consultar Vistas
-- ============================================
PRINT '';
PRINT '--- PRUEBA 6: Consultar Vistas ---';


-- Vista de Consultorios Activos
PRINT 'Consultorios Activos:';
SELECT * FROM HospitalOne.VW_ConsultoriosActivos;


-- Vista de Estadísticas de Doctores
PRINT 'Estadísticas de Doctores:';
SELECT * FROM HospitalOne.VW_EstadisticasDoctores;


-- Vista de Citas del Día
PRINT 'Citas del Día:';
SELECT * FROM HospitalOne.VW_CitasDelDia;


-- ============================================
-- PRUEBA 7: Liberar Consultorios
-- ============================================
PRINT '';
PRINT '--- PRUEBA 7: Liberar Consultorios ---';


EXEC HospitalOne.SP_LiberarConsultorio @ConsultorioID = 1;


EXEC HospitalOne.SP_LiberarConsultorio @ConsultorioID = 2;


-- Verificar estado después de liberar
PRINT 'Estado de Doctores después de liberar consultorios:';
SELECT DoctorID, Nombres + ' ' + Apellidos AS NombreDoctor, EstadoDisponibilidad 
FROM HospitalOne.Doctores 
WHERE DoctorID IN (1, 2);


PRINT 'Estado de Consultorios liberados:';
SELECT ConsultorioID, NumeroConsultorio, EstadoConsultorio, DoctorAsignadoID 
FROM HospitalOne.Consultorios 
WHERE ConsultorioID IN (1, 2);


-- ============================================
-- PRUEBA 8: Validaciones y Manejo de Errores
-- ============================================
PRINT '';
PRINT '--- PRUEBA 8: Validaciones y Manejo de Errores ---';


-- Intentar asignar un doctor que no está disponible (debería fallar)
PRINT 'Intentando asignar doctor no disponible...';
BEGIN TRY
    EXEC HospitalOne.SP_AsignarDoctorConsultorio 
        @DoctorID = 3,  -- Este doctor está en Urgencias
        @ConsultorioID = 4, 
        @TipoServicio = 'Citas';
END TRY
BEGIN CATCH
    PRINT 'Error capturado correctamente: ' + ERROR_MESSAGE();
END CATCH


-- Intentar iniciar una cita que no está en estado Programada
PRINT 'Intentando iniciar cita ya completada...';
BEGIN TRY
    EXEC HospitalOne.SP_IniciarCita @CitaID = 1;  -- Ya está completada
END TRY
BEGIN CATCH
    PRINT 'Error capturado correctamente: ' + ERROR_MESSAGE();
END CATCH


-- ============================================
-- PRUEBA 9: Crear más citas para pruebas estadísticas
-- ============================================
PRINT '';
PRINT '--- PRUEBA 9: Crear Citas Adicionales ---';


-- Reasignar doctores
EXEC HospitalOne.SP_AsignarDoctorConsultorio 
    @DoctorID = 1, 
    @ConsultorioID = 1, 
    @TipoServicio = 'Citas';


EXEC HospitalOne.SP_AsignarDoctorConsultorio 
    @DoctorID = 4, 
    @ConsultorioID = 4, 
    @TipoServicio = 'Citas';


-- Crear nuevas citas
DECLARE @NuevaCita1 INT, @NuevaCita2 INT;

EXEC HospitalOne.SP_CrearCita
    @ClienteID = 5,
    @DoctorID = 1,
    @ConsultorioID = 1,
    @EspecialidadID = 1,
    @FechaCita = '2024-11-05 14:00:00',
    @TipoCita = 'Programada',
    @Motivo = 'Consulta por dolor de espalda',
    @CitaID = @NuevaCita1 OUTPUT;


EXEC HospitalOne.SP_CrearCita
    @ClienteID = 6,
    @DoctorID = 4,
    @ConsultorioID = 4,
    @EspecialidadID = 4,
    @FechaCita = '2024-11-05 15:00:00',
    @TipoCita = 'Programada',
    @Motivo = 'Evaluación de lesión en rodilla',
    @CitaID = @NuevaCita2 OUTPUT;


-- Procesar estas citas
EXEC HospitalOne.SP_IniciarCita @CitaID = 4;
WAITFOR DELAY '00:00:01';
EXEC HospitalOne.SP_CompletarCita 
    @CitaID = 4,
    @Diagnostico = 'Contractura muscular leve. Tratamiento con analgésicos.',
    @Observaciones = 'Reposo relativo por 5 días';


EXEC HospitalOne.SP_IniciarCita @CitaID = 5;
WAITFOR DELAY '00:00:01';
EXEC HospitalOne.SP_CompletarCita 
    @CitaID = 5,
    @Diagnostico = 'Esguince grado 1 en rodilla derecha. Inmovilización parcial.',
    @Observaciones = 'Fisioterapia en 2 semanas';


-- ============================================
-- RESUMEN FINAL
-- ============================================
PRINT '';
PRINT '============================================';
PRINT 'RESUMEN DE PRUEBAS COMPLETADAS';
PRINT '============================================';


PRINT 'Total de Citas Creadas:';
SELECT COUNT(*) AS TotalCitas FROM HospitalOne.Citas;


PRINT 'Total de Citas Completadas:';
SELECT COUNT(*) AS CitasCompletadas FROM HospitalOne.Citas WHERE EstadoCita = 'Completada';


PRINT 'Total de Citas Canceladas:';
SELECT COUNT(*) AS CitasCanceladas FROM HospitalOne.Citas WHERE EstadoCita = 'Cancelada';


PRINT 'Registros en Histórico de Citas:';
SELECT COUNT(*) AS RegistrosHistorico FROM HospitalOne.Citas_Historico;


PRINT 'Consultorios Actualmente en Uso:';
SELECT COUNT(*) AS ConsultoriosEnUso FROM HospitalOne.Consultorios WHERE EstadoConsultorio IN ('Citas', 'Urgencias');


PRINT 'Doctores Disponibles:';
SELECT COUNT(*) AS DoctoresDisponibles FROM HospitalOne.Doctores WHERE EstadoDisponibilidad = 'Disponible';


PRINT '';
PRINT '============================================';
PRINT 'TODAS LAS PRUEBAS COMPLETADAS EXITOSAMENTE';
PRINT '============================================';

