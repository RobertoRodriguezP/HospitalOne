IF DB_ID(N'Hospital') IS NULL
BEGIN
  CREATE DATABASE [Hospital];
END


USE [Hospital];


IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'HospitalOne')
BEGIN
    EXEC('CREATE SCHEMA HospitalOne');
    PRINT 'Esquema HospitalOne creado exitosamente';
END
ELSE
BEGIN
    PRINT 'Esquema HospitalOne ya existe';
END


-- ============================================
-- Script 2: Creación de Tablas
-- Base de Datos: Hospital
-- Esquema: HospitalOne
-- ============================================



-- ============================================
-- Tabla: Especialidades
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'HospitalOne.Especialidades') AND type = 'U')
BEGIN
    CREATE TABLE HospitalOne.Especialidades
    (
        EspecialidadID INT IDENTITY(1,1) PRIMARY KEY,
        NombreEspecialidad NVARCHAR(100) UNIQUE NOT NULL,
        Descripcion NVARCHAR(500),
        TiempoPromedioConsulta INT NOT NULL, -- En minutos
        Activo BIT DEFAULT 1,
        FechaCreacion DATETIME DEFAULT GETDATE()
    );
    PRINT 'Tabla Especialidades creada exitosamente';
END
ELSE
BEGIN
    PRINT 'Tabla Especialidades ya existe';
END


-- ============================================
-- Tabla: Clientes
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'HospitalOne.Clientes') AND type = 'U')
BEGIN
    CREATE TABLE HospitalOne.Clientes
    (
        ClienteID INT IDENTITY(1,1) PRIMARY KEY,
        Nombres NVARCHAR(100) NOT NULL,
        Apellidos NVARCHAR(100) NOT NULL,
        FechaNacimiento DATE NOT NULL,
        Genero CHAR(1) CHECK (Genero IN ('M', 'F', 'O')),
        Telefono NVARCHAR(20),
        Email NVARCHAR(100),
        Direccion NVARCHAR(200),
        DocumentoIdentidad NVARCHAR(50) UNIQUE NOT NULL,
        TipoDocumento NVARCHAR(20) NOT NULL, -- Cédula, Pasaporte, etc.
        FechaRegistro DATETIME DEFAULT GETDATE(),
        Activo BIT DEFAULT 1,
        CONSTRAINT CHK_Email_Cliente CHECK (Email LIKE '%@%.%')
    );
    PRINT 'Tabla Clientes creada exitosamente';
END
ELSE
BEGIN
    PRINT 'Tabla Clientes ya existe';
END


-- ============================================
-- Tabla: Doctores
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'HospitalOne.Doctores') AND type = 'U')
BEGIN
    CREATE TABLE HospitalOne.Doctores
    (
        DoctorID INT IDENTITY(1,1) PRIMARY KEY,
        Nombres NVARCHAR(100) NOT NULL,
        Apellidos NVARCHAR(100) NOT NULL,
        DocumentoIdentidad NVARCHAR(50) UNIQUE NOT NULL,
        Telefono NVARCHAR(20),
        Email NVARCHAR(100),
        NumeroLicencia NVARCHAR(50) UNIQUE NOT NULL,
        EspecialidadID INT NOT NULL,
        EstadoDisponibilidad NVARCHAR(20) DEFAULT 'Disponible' CHECK (EstadoDisponibilidad IN ('Disponible', 'No Disponible', 'En Consulta', 'En Ocio')),
        -- Disponible: Listo para ser asignado
        -- No Disponible: Fuera de servicio (vacaciones, enfermo, etc.)
        -- En Consulta: Atendiendo activamente a un paciente
        -- En Ocio: Asignado a consultorio pero sin atender paciente
        FechaContratacion DATE NOT NULL,
        Activo BIT DEFAULT 1,
        FechaRegistro DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_Doctor_Especialidad FOREIGN KEY (EspecialidadID) REFERENCES HospitalOne.Especialidades(EspecialidadID),
        CONSTRAINT CHK_Email_Doctor CHECK (Email LIKE '%@%.%')
    );
    PRINT 'Tabla Doctores creada exitosamente';
END
ELSE
BEGIN
    PRINT 'Tabla Doctores ya existe';
END

-- ============================================
-- Tabla: Consultorios
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'HospitalOne.Consultorios') AND type = 'U')
BEGIN
    CREATE TABLE HospitalOne.Consultorios
    (
        ConsultorioID INT IDENTITY(1,1) PRIMARY KEY,
        NumeroConsultorio NVARCHAR(10) UNIQUE NOT NULL,
        Piso INT NOT NULL,
        EdificioAla NVARCHAR(50), -- Ala A, B, C, etc.
        TipoConsultorio NVARCHAR(50), -- General, Especializado, Urgencias
        EstadoConsultorio NVARCHAR(20) DEFAULT 'Disponible' CHECK (EstadoConsultorio IN ('Disponible', 'No Disponible', 'Citas', 'Urgencias')),
        -- Disponible: Listo para asignar doctor/paciente
        -- No Disponible: Fuera de servicio (mantenimiento, limpieza)
        -- Citas: Consultorio activo atendiendo citas programadas
        -- Urgencias: Consultorio activo atendiendo urgencias
        DoctorAsignadoID INT NULL,
        FechaAsignacionDoctor DATETIME NULL,
        Activo BIT DEFAULT 1,
        FechaCreacion DATETIME DEFAULT GETDATE(),
        UltimaActualizacion DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_Consultorio_Doctor FOREIGN KEY (DoctorAsignadoID) REFERENCES HospitalOne.Doctores(DoctorID)
    );
    PRINT 'Tabla Consultorios creada exitosamente';
END
ELSE
BEGIN
    PRINT 'Tabla Consultorios ya existe';
END


-- ============================================
-- Tabla: Citas
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'HospitalOne.Citas') AND type = 'U')
BEGIN
    CREATE TABLE HospitalOne.Citas
    (
        CitaID INT IDENTITY(1,1) PRIMARY KEY,
        ClienteID INT NOT NULL,
        DoctorID INT NOT NULL,
        ConsultorioID INT NOT NULL,
        EspecialidadID INT NOT NULL,
        FechaCita DATETIME NOT NULL,
        FechaFinEstimada DATETIME NOT NULL,
        FechaInicioReal DATETIME NULL,
        FechaFinReal DATETIME NULL,
        TiempoDuracionMinutos AS DATEDIFF(MINUTE, FechaInicioReal, FechaFinReal), -- Columna calculada
        TipoCita NVARCHAR(20) DEFAULT 'Programada' CHECK (TipoCita IN ('Programada', 'Urgencia')),
        EstadoCita NVARCHAR(20) DEFAULT 'Programada' CHECK (EstadoCita IN ('Programada', 'En Curso', 'Completada', 'Cancelada', 'No Asistió')),
        Motivo NVARCHAR(500),
        Diagnostico NVARCHAR(1000) NULL,
        Observaciones NVARCHAR(1000) NULL,
        FechaRegistro DATETIME DEFAULT GETDATE(),
        UsuarioRegistro NVARCHAR(100),
        CONSTRAINT FK_Cita_Cliente FOREIGN KEY (ClienteID) REFERENCES HospitalOne.Clientes(ClienteID),
        CONSTRAINT FK_Cita_Doctor FOREIGN KEY (DoctorID) REFERENCES HospitalOne.Doctores(DoctorID),
        CONSTRAINT FK_Cita_Consultorio FOREIGN KEY (ConsultorioID) REFERENCES HospitalOne.Consultorios(ConsultorioID),
        CONSTRAINT FK_Cita_Especialidad FOREIGN KEY (EspecialidadID) REFERENCES HospitalOne.Especialidades(EspecialidadID),
        CONSTRAINT CHK_FechaFinEstimada CHECK (FechaFinEstimada > FechaCita)
    );
    PRINT 'Tabla Citas creada exitosamente';
END
ELSE
BEGIN
    PRINT 'Tabla Citas ya existe';
END


-- ============================================
-- Tabla: Consultorios_Historico
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'HospitalOne.Consultorios_Historico') AND type = 'U')
BEGIN
    CREATE TABLE HospitalOne.Consultorios_Historico
    (
        HistoricoID INT IDENTITY(1,1) PRIMARY KEY,
        ConsultorioID INT NOT NULL,
        NumeroConsultorio NVARCHAR(10) NOT NULL,
        EstadoAnterior NVARCHAR(20),
        EstadoNuevo NVARCHAR(20),
        DoctorAsignadoID INT NULL,
        DoctorAnteriorID INT NULL,
        FechaCambio DATETIME DEFAULT GETDATE(),
        UsuarioCambio NVARCHAR(100),
        Motivo NVARCHAR(500),
        CONSTRAINT FK_ConsultorioHist_Consultorio FOREIGN KEY (ConsultorioID) REFERENCES HospitalOne.Consultorios(ConsultorioID)
    );
    PRINT 'Tabla Consultorios_Historico creada exitosamente';
END
ELSE
BEGIN
    PRINT 'Tabla Consultorios_Historico ya existe';
END


-- ============================================
-- Tabla: Citas_Historico
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'HospitalOne.Citas_Historico') AND type = 'U')
BEGIN
    CREATE TABLE HospitalOne.Citas_Historico
    (
        HistoricoCitaID INT IDENTITY(1,1) PRIMARY KEY,
        CitaID INT NOT NULL,
        ClienteID INT NOT NULL,
        DoctorID INT NOT NULL,
        ConsultorioID INT NOT NULL,
        EspecialidadID INT NOT NULL,
        FechaCita DATETIME NOT NULL,
        FechaInicioReal DATETIME NULL,
        FechaFinReal DATETIME NULL,
        TiempoDuracionMinutos INT NULL,
        TiempoEsperaMinutos INT NULL, -- Tiempo entre cita programada y inicio real
        EstadoCita NVARCHAR(20),
        Diagnostico NVARCHAR(1000) NULL,
        FechaArchivado DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_CitaHist_Cita FOREIGN KEY (CitaID) REFERENCES HospitalOne.Citas(CitaID),
        CONSTRAINT FK_CitaHist_Cliente FOREIGN KEY (ClienteID) REFERENCES HospitalOne.Clientes(ClienteID),
        CONSTRAINT FK_CitaHist_Doctor FOREIGN KEY (DoctorID) REFERENCES HospitalOne.Doctores(DoctorID),
        CONSTRAINT FK_CitaHist_Consultorio FOREIGN KEY (ConsultorioID) REFERENCES HospitalOne.Consultorios(ConsultorioID)
    );
    PRINT 'Tabla Citas_Historico creada exitosamente';
END
ELSE
BEGIN
    PRINT 'Tabla Citas_Historico ya existe';
END


PRINT '============================================';
PRINT 'Todas las tablas han sido creadas exitosamente';
PRINT '============================================';



-- ============================================
-- Script 3: Creación de Índices
-- Base de Datos: Hospital
-- Esquema: HospitalOne
-- ============================================



-- ============================================
-- Índice: Consultorios_Historico por Consultorio y Fecha
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ConsultoriosHist_Consultorio_Fecha' 
               AND object_id = OBJECT_ID('HospitalOne.Consultorios_Historico'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_ConsultoriosHist_Consultorio_Fecha 
    ON HospitalOne.Consultorios_Historico(ConsultorioID, FechaCambio);
    PRINT 'Índice IX_ConsultoriosHist_Consultorio_Fecha creado exitosamente';
END
ELSE
BEGIN
    PRINT 'Índice IX_ConsultoriosHist_Consultorio_Fecha ya existe';
END


-- ============================================
-- Índice: Citas_Historico por Doctor y Fecha
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CitasHist_Doctor_Fecha' 
               AND object_id = OBJECT_ID('HospitalOne.Citas_Historico'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_CitasHist_Doctor_Fecha 
    ON HospitalOne.Citas_Historico(DoctorID, FechaCita);
    PRINT 'Índice IX_CitasHist_Doctor_Fecha creado exitosamente';
END
ELSE
BEGIN
    PRINT 'Índice IX_CitasHist_Doctor_Fecha ya existe';
END


-- ============================================
-- Índice: Citas_Historico por Consultorio y Fecha
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CitasHist_Consultorio_Fecha' 
               AND object_id = OBJECT_ID('HospitalOne.Citas_Historico'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_CitasHist_Consultorio_Fecha 
    ON HospitalOne.Citas_Historico(ConsultorioID, FechaCita);
    PRINT 'Índice IX_CitasHist_Consultorio_Fecha creado exitosamente';
END
ELSE
BEGIN
    PRINT 'Índice IX_CitasHist_Consultorio_Fecha ya existe';
END


PRINT '============================================';
PRINT 'Todos los índices han sido creados exitosamente';
PRINT '============================================';

-- ============================================
-- Trigger: Actualizar Estado Doctor al asignar/liberar Consultorio
-- ============================================
CREATE OR ALTER TRIGGER HospitalOne.TRG_ActualizarEstadoDoctor_Consultorio
ON HospitalOne.Consultorios
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Si se asigna un doctor a un consultorio activo, cambiar su estado
    UPDATE d
    SET EstadoDisponibilidad = CASE 
        WHEN i.EstadoConsultorio IN ('Citas', 'Urgencias') THEN 'En Ocio'
        ELSE 'Disponible'
    END
    FROM HospitalOne.Doctores d
    INNER JOIN inserted i ON d.DoctorID = i.DoctorAsignadoID
    WHERE i.DoctorAsignadoID IS NOT NULL;
    
    -- Si se libera un doctor (se quita de consultorio), ponerlo disponible
    UPDATE d
    SET EstadoDisponibilidad = 'Disponible'
    FROM HospitalOne.Doctores d
    INNER JOIN deleted del ON d.DoctorID = del.DoctorAsignadoID
    WHERE del.DoctorAsignadoID IS NOT NULL 
      AND NOT EXISTS (SELECT 1 FROM inserted i WHERE i.DoctorAsignadoID = del.DoctorAsignadoID);
END;
PRINT 'Trigger TRG_ActualizarEstadoDoctor_Consultorio creado exitosamente';


-- ============================================
-- Trigger: Registrar cambios en Consultorios_Historico
-- ============================================
CREATE OR ALTER TRIGGER HospitalOne.TRG_Consultorios_Historico
ON HospitalOne.Consultorios
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO HospitalOne.Consultorios_Historico 
    (ConsultorioID, NumeroConsultorio, EstadoAnterior, EstadoNuevo, DoctorAsignadoID, DoctorAnteriorID, UsuarioCambio)
    SELECT 
        i.ConsultorioID,
        i.NumeroConsultorio,
        d.EstadoConsultorio,
        i.EstadoConsultorio,
        i.DoctorAsignadoID,
        d.DoctorAsignadoID,
        SYSTEM_USER
    FROM inserted i
    INNER JOIN deleted d ON i.ConsultorioID = d.ConsultorioID
    WHERE i.EstadoConsultorio <> d.EstadoConsultorio 
       OR ISNULL(i.DoctorAsignadoID, 0) <> ISNULL(d.DoctorAsignadoID, 0);
END;
PRINT 'Trigger TRG_Consultorios_Historico creado exitosamente';


-- ============================================
-- Trigger: Archivar Citas completadas o canceladas
-- ============================================
CREATE OR ALTER TRIGGER HospitalOne.TRG_Citas_Historico
ON HospitalOne.Citas
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Solo archivar cuando la cita se completa o cancela
    INSERT INTO HospitalOne.Citas_Historico 
    (CitaID, ClienteID, DoctorID, ConsultorioID, EspecialidadID, FechaCita, 
     FechaInicioReal, FechaFinReal, TiempoDuracionMinutos, TiempoEsperaMinutos, EstadoCita, Diagnostico)
    SELECT 
        i.CitaID,
        i.ClienteID,
        i.DoctorID,
        i.ConsultorioID,
        i.EspecialidadID,
        i.FechaCita,
        i.FechaInicioReal,
        i.FechaFinReal,
        DATEDIFF(MINUTE, i.FechaInicioReal, i.FechaFinReal),
        DATEDIFF(MINUTE, i.FechaCita, i.FechaInicioReal),
        i.EstadoCita,
        i.Diagnostico
    FROM inserted i
    INNER JOIN deleted d ON i.CitaID = d.CitaID
    WHERE i.EstadoCita IN ('Completada', 'Cancelada', 'No Asistió')
      AND d.EstadoCita NOT IN ('Completada', 'Cancelada', 'No Asistió');
END;
PRINT 'Trigger TRG_Citas_Historico creado exitosamente';


-- ============================================
-- Trigger: Actualizar Estado Doctor según estado de Cita
-- ============================================
CREATE OR ALTER TRIGGER HospitalOne.TRG_ActualizarEstadoDoctor_Cita
ON HospitalOne.Citas
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Cuando una cita inicia, cambiar doctor a "En Consulta"
    UPDATE d
    SET EstadoDisponibilidad = 'En Consulta'
    FROM HospitalOne.Doctores d
    INNER JOIN inserted i ON d.DoctorID = i.DoctorID
    WHERE i.EstadoCita = 'En Curso' AND i.FechaInicioReal IS NOT NULL;
    
    -- Cuando una cita termina, cambiar doctor a "En Ocio" si aún está en consultorio
    UPDATE d
    SET EstadoDisponibilidad = CASE 
        WHEN EXISTS (
            SELECT 1 FROM HospitalOne.Consultorios c 
            WHERE c.DoctorAsignadoID = d.DoctorID 
              AND c.EstadoConsultorio IN ('Citas', 'Urgencias')
        ) THEN 'En Ocio'
        ELSE 'Disponible'
    END
    FROM HospitalOne.Doctores d
    INNER JOIN inserted i ON d.DoctorID = i.DoctorID
    WHERE i.EstadoCita IN ('Completada', 'Cancelada', 'No Asistió');
END;
PRINT 'Trigger TRG_ActualizarEstadoDoctor_Cita creado exitosamente';



-- ============================================
-- Script 5: Creación de Vistas
-- Base de Datos: Hospital
-- Esquema: HospitalOne
-- ============================================

-- ============================================
-- Vista: Consultorios Activos con información del Doctor
-- ============================================
CREATE OR ALTER VIEW HospitalOne.VW_ConsultoriosActivos
AS
SELECT 
    c.ConsultorioID,
    c.NumeroConsultorio,
    c.Piso,
    c.EdificioAla,
    c.TipoConsultorio,
    c.EstadoConsultorio,
    d.DoctorID,
    d.Nombres + ' ' + d.Apellidos AS NombreDoctor,
    e.NombreEspecialidad,
    c.FechaAsignacionDoctor,
    DATEDIFF(MINUTE, c.FechaAsignacionDoctor, GETDATE()) AS MinutosAsignado
FROM HospitalOne.Consultorios c
LEFT JOIN HospitalOne.Doctores d ON c.DoctorAsignadoID = d.DoctorID
LEFT JOIN HospitalOne.Especialidades e ON d.EspecialidadID = e.EspecialidadID
WHERE c.Activo = 1;


-- ============================================
-- Vista: Estadísticas de Doctores
-- ============================================
CREATE OR ALTER VIEW HospitalOne.VW_EstadisticasDoctores
AS
SELECT 
    d.DoctorID,
    d.Nombres + ' ' + d.Apellidos AS NombreDoctor,
    e.NombreEspecialidad,
    d.EstadoDisponibilidad,
    COUNT(ch.CitaID) AS TotalCitasCompletadas,
    AVG(ch.TiempoDuracionMinutos) AS PromedioMinutosPorCita,
    AVG(ch.TiempoEsperaMinutos) AS PromedioTiempoEspera
FROM HospitalOne.Doctores d
INNER JOIN HospitalOne.Especialidades e ON d.EspecialidadID = e.EspecialidadID
LEFT JOIN HospitalOne.Citas_Historico ch ON d.DoctorID = ch.DoctorID
WHERE d.Activo = 1
GROUP BY d.DoctorID, d.Nombres, d.Apellidos, e.NombreEspecialidad, d.EstadoDisponibilidad;



-- ============================================
-- Vista: Citas del Día
-- ============================================
CREATE OR ALTER VIEW HospitalOne.VW_CitasDelDia
AS
SELECT 
    ci.CitaID,
    cl.Nombres + ' ' + cl.Apellidos AS NombreCliente,
    d.Nombres + ' ' + d.Apellidos AS NombreDoctor,
    e.NombreEspecialidad,
    co.NumeroConsultorio,
    ci.FechaCita,
    ci.EstadoCita,
    ci.TipoCita,
    ci.Motivo
FROM HospitalOne.Citas ci
INNER JOIN HospitalOne.Clientes cl ON ci.ClienteID = cl.ClienteID
INNER JOIN HospitalOne.Doctores d ON ci.DoctorID = d.DoctorID
INNER JOIN HospitalOne.Especialidades e ON ci.EspecialidadID = e.EspecialidadID
INNER JOIN HospitalOne.Consultorios co ON ci.ConsultorioID = co.ConsultorioID
WHERE CAST(ci.FechaCita AS DATE) = CAST(GETDATE() AS DATE)
  AND ci.EstadoCita NOT IN ('Cancelada');


-- ============================================
-- Script 6: Creación de Procedimientos Almacenados
-- Base de Datos: Hospital
-- Esquema: HospitalOne
-- ============================================

-- ============================================
-- SP: Asignar Doctor a Consultorio
-- ============================================
CREATE OR ALTER PROCEDURE HospitalOne.SP_AsignarDoctorConsultorio
    @DoctorID INT,
    @ConsultorioID INT,
    @TipoServicio NVARCHAR(20) -- 'Citas' o 'Urgencias'
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Validar que el doctor esté disponible
        IF NOT EXISTS (SELECT 1 FROM HospitalOne.Doctores WHERE DoctorID = @DoctorID AND EstadoDisponibilidad = 'Disponible')
        BEGIN
            RAISERROR('El doctor no está disponible', 16, 1);
            RETURN;
        END
        
        -- Validar que el consultorio esté disponible
        IF NOT EXISTS (SELECT 1 FROM HospitalOne.Consultorios WHERE ConsultorioID = @ConsultorioID AND EstadoConsultorio = 'Disponible')
        BEGIN
            RAISERROR('El consultorio no está disponible', 16, 1);
            RETURN;
        END
        
        -- Asignar doctor al consultorio
        UPDATE HospitalOne.Consultorios
        SET DoctorAsignadoID = @DoctorID,
            EstadoConsultorio = @TipoServicio,
            FechaAsignacionDoctor = GETDATE(),
            UltimaActualizacion = GETDATE()
        WHERE ConsultorioID = @ConsultorioID;
        
        COMMIT TRANSACTION;
        PRINT 'Doctor asignado exitosamente al consultorio';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;

PRINT 'Procedimiento SP_AsignarDoctorConsultorio creado exitosamente';


-- ============================================
-- SP: Liberar Consultorio
-- ============================================
CREATE OR ALTER PROCEDURE HospitalOne.SP_LiberarConsultorio
    @ConsultorioID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        UPDATE HospitalOne.Consultorios
        SET DoctorAsignadoID = NULL,
            EstadoConsultorio = 'Disponible',
            UltimaActualizacion = GETDATE()
        WHERE ConsultorioID = @ConsultorioID;
        
        COMMIT TRANSACTION;
        PRINT 'Consultorio liberado exitosamente';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
PRINT 'Procedimiento SP_LiberarConsultorio creado exitosamente';

-- ============================================
-- SP: Crear Cita
-- ============================================
CREATE OR ALTER PROCEDURE HospitalOne.SP_CrearCita
    @ClienteID INT,
    @DoctorID INT,
    @ConsultorioID INT,
    @EspecialidadID INT,
    @FechaCita DATETIME,
    @TipoCita NVARCHAR(20),
    @Motivo NVARCHAR(500),
    @CitaID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Validar que el cliente existe y está activo
        IF NOT EXISTS (SELECT 1 FROM HospitalOne.Clientes WHERE ClienteID = @ClienteID AND Activo = 1)
        BEGIN
            RAISERROR('El cliente no existe o está inactivo', 16, 1);
            RETURN;
        END
        
        -- Validar que el doctor existe y está activo
        IF NOT EXISTS (SELECT 1 FROM HospitalOne.Doctores WHERE DoctorID = @DoctorID AND Activo = 1)
        BEGIN
            RAISERROR('El doctor no existe o está inactivo', 16, 1);
            RETURN;
        END
        
        -- Calcular fecha fin estimada basada en el tiempo promedio de la especialidad
        DECLARE @TiempoPromedio INT;
        SELECT @TiempoPromedio = TiempoPromedioConsulta 
        FROM HospitalOne.Especialidades 
        WHERE EspecialidadID = @EspecialidadID;
        
        DECLARE @FechaFinEstimada DATETIME = DATEADD(MINUTE, @TiempoPromedio, @FechaCita);
        
        -- Insertar la cita
        INSERT INTO HospitalOne.Citas (ClienteID, DoctorID, ConsultorioID, EspecialidadID, FechaCita, FechaFinEstimada, TipoCita, Motivo, UsuarioRegistro)
        VALUES (@ClienteID, @DoctorID, @ConsultorioID, @EspecialidadID, @FechaCita, @FechaFinEstimada, @TipoCita, @Motivo, SYSTEM_USER);
        
        SET @CitaID = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION;
        PRINT 'Cita creada exitosamente con ID: ' + CAST(@CitaID AS NVARCHAR(10));
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
PRINT 'Procedimiento SP_CrearCita creado exitosamente';

-- ============================================
-- SP: Iniciar Cita
-- ============================================
CREATE OR ALTER PROCEDURE HospitalOne.SP_IniciarCita
    @CitaID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Validar que la cita existe y está programada
        IF NOT EXISTS (SELECT 1 FROM HospitalOne.Citas WHERE CitaID = @CitaID AND EstadoCita = 'Programada')
        BEGIN
            RAISERROR('La cita no existe o no está en estado Programada', 16, 1);
            RETURN;
        END
        
        -- Actualizar estado de la cita
        UPDATE HospitalOne.Citas
        SET EstadoCita = 'En Curso',
            FechaInicioReal = GETDATE()
        WHERE CitaID = @CitaID;
        
        COMMIT TRANSACTION;
        PRINT 'Cita iniciada exitosamente';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
PRINT 'Procedimiento SP_IniciarCita creado exitosamente';

-- ============================================
-- SP: Completar Cita
-- ============================================
CREATE OR ALTER PROCEDURE HospitalOne.SP_CompletarCita
    @CitaID INT,
    @Diagnostico NVARCHAR(1000),
    @Observaciones NVARCHAR(1000) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Validar que la cita existe y está en curso
        IF NOT EXISTS (SELECT 1 FROM HospitalOne.Citas WHERE CitaID = @CitaID AND EstadoCita = 'En Curso')
        BEGIN
            RAISERROR('La cita no existe o no está en estado En Curso', 16, 1);
            RETURN;
        END
        
        -- Actualizar estado de la cita
        UPDATE HospitalOne.Citas
        SET EstadoCita = 'Completada',
            FechaFinReal = GETDATE(),
            Diagnostico = @Diagnostico,
            Observaciones = @Observaciones
        WHERE CitaID = @CitaID;
        
        COMMIT TRANSACTION;
        PRINT 'Cita completada exitosamente';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
PRINT 'Procedimiento SP_CompletarCita creado exitosamente';

-- ============================================
-- SP: Cancelar Cita
-- ============================================
CREATE OR ALTER PROCEDURE HospitalOne.SP_CancelarCita
    @CitaID INT,
    @Motivo NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Validar que la cita existe
        IF NOT EXISTS (SELECT 1 FROM HospitalOne.Citas WHERE CitaID = @CitaID)
        BEGIN
            RAISERROR('La cita no existe', 16, 1);
            RETURN;
        END
        
        -- Actualizar estado de la cita
        UPDATE HospitalOne.Citas
        SET EstadoCita = 'Cancelada',
            Observaciones = @Motivo
        WHERE CitaID = @CitaID;
        
        COMMIT TRANSACTION;
        PRINT 'Cita cancelada exitosamente';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
PRINT 'Procedimiento SP_CancelarCita creado exitosamente';

PRINT '============================================';
PRINT 'Todos los procedimientos almacenados han sido creados exitosamente';
PRINT '============================================';





