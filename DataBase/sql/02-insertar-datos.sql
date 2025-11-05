SET IDENTITY_INSERT HospitalOne.Especialidades ON;

INSERT INTO HospitalOne.Especialidades (EspecialidadID, NombreEspecialidad, Descripcion, TiempoPromedioConsulta)
VALUES 
(1, 'Medicina General', 'Atención médica general y preventiva', 30),
(2, 'Cardiología', 'Especialidad del corazón y sistema cardiovascular', 45),
(3, 'Pediatría', 'Atención médica para niños y adolescentes', 30),
(4, 'Traumatología', 'Tratamiento de lesiones del sistema musculoesquelético', 40),
(5, 'Ginecología', 'Salud reproductiva femenina', 35),
(6, 'Dermatología', 'Enfermedades de la piel', 25),
(7, 'Oftalmología', 'Enfermedades de los ojos', 30),
(8, 'Neurología', 'Enfermedades del sistema nervioso', 45);

SET IDENTITY_INSERT HospitalOne.Especialidades OFF;

-- ============================================
-- Insertar Doctores
-- ============================================
PRINT 'Insertando doctores...';

SET IDENTITY_INSERT HospitalOne.Doctores ON;

INSERT INTO HospitalOne.Doctores (DoctorID, Nombres, Apellidos, DocumentoIdentidad, Telefono, Email, NumeroLicencia, EspecialidadID, FechaContratacion)
VALUES 
(1, 'Carlos', 'Méndez Ruiz', '8-123-4567', '6555-1234', 'cmendez@hospitalOne.com', 'LIC-001-2020', 1, '2020-01-15'),
(2, 'Ana María', 'Rodríguez Pérez', '8-234-5678', '6555-2345', 'arodriguez@hospitalOne.com', 'LIC-002-2019', 2, '2019-06-20'),
(3, 'José', 'González López', '8-345-6789', '6555-3456', 'jgonzalez@hospitalOne.com', 'LIC-003-2021', 3, '2021-03-10'),
(4, 'María', 'Fernández Castro', '8-456-7890', '6555-4567', 'mfernandez@hospitalOne.com', 'LIC-004-2018', 4, '2018-11-05'),
(5, 'Roberto', 'Sánchez Mora', '8-567-8901', '6555-5678', 'rsanchez@hospitalOne.com', 'LIC-005-2022', 5, '2022-02-14'),
(6, 'Patricia', 'Torres Vega', '8-678-9012', '6555-6789', 'ptorres@hospitalOne.com', 'LIC-006-2020', 6, '2020-07-22'),
(7, 'Luis', 'Ramírez Silva', '8-789-0123', '6555-7890', 'lramirez@hospitalOne.com', 'LIC-007-2021', 7, '2021-09-15'),
(8, 'Carmen', 'Díaz Morales', '8-890-1234', '6555-8901', 'cdiaz@hospitalOne.com', 'LIC-008-2019', 8, '2019-04-18');

SET IDENTITY_INSERT HospitalOne.Doctores OFF;

PRINT 'Doctores insertados: 8 registros';

-- ============================================
-- Insertar Consultorios
-- ============================================
PRINT 'Insertando consultorios...';

SET IDENTITY_INSERT HospitalOne.Consultorios ON;

INSERT INTO HospitalOne.Consultorios (ConsultorioID, NumeroConsultorio, Piso, EdificioAla, TipoConsultorio)
VALUES 
(1, 'C-101', 1, 'Ala A', 'General'),
(2, 'C-102', 1, 'Ala A', 'Especializado'),
(3, 'C-103', 1, 'Ala A', 'Urgencias'),
(4, 'C-201', 2, 'Ala B', 'Especializado'),
(5, 'C-202', 2, 'Ala B', 'General'),
(6, 'C-301', 3, 'Ala C', 'Urgencias'),
(7, 'C-302', 3, 'Ala C', 'Especializado'),
(8, 'C-401', 4, 'Ala D', 'General');

SET IDENTITY_INSERT HospitalOne.Consultorios OFF;

-- ============================================
-- Insertar Clientes
-- ============================================
PRINT 'Insertando clientes...';

SET IDENTITY_INSERT HospitalOne.Clientes ON;

INSERT INTO HospitalOne.Clientes (ClienteID, Nombres, Apellidos, FechaNacimiento, Genero, Telefono, Email, Direccion, DocumentoIdentidad, TipoDocumento)
VALUES 
(1, 'Juan', 'Pérez García', '1985-05-15', 'M', '6777-1111', 'jperez@email.com', 'Calle 50, Ciudad de Panamá', '8-111-2222', 'Cédula'),
(2, 'Laura', 'Martínez Silva', '1990-08-20', 'F', '6777-2222', 'lmartinez@email.com', 'Avenida Balboa, Panamá', '8-222-3333', 'Cédula'),
(3, 'Pedro', 'López Ramírez', '1978-12-10', 'M', '6777-3333', 'plopez@email.com', 'San Miguelito, Panamá', '8-333-4444', 'Cédula'),
(4, 'Sofia', 'Hernández Ortiz', '2010-03-25', 'F', '6777-4444', 'shernandez@email.com', 'Arraiján, Panamá Oeste', '8-444-5555', 'Cédula'),
(5, 'Miguel', 'Castro Ruiz', '1995-11-30', 'M', '6777-5555', 'mcastro@email.com', 'Chorrera, Panamá Oeste', '8-555-6666', 'Cédula'),
(6, 'Andrea', 'Morales Gómez', '1988-07-12', 'F', '6777-6666', 'amorales@email.com', 'Bethania, Ciudad de Panamá', '8-666-7777', 'Cédula'),
(7, 'Ricardo', 'Vargas León', '1972-02-28', 'M', '6777-7777', 'rvargas@email.com', 'El Dorado, Ciudad de Panamá', '8-777-8888', 'Cédula'),
(8, 'Isabel', 'Jiménez Cruz', '2005-09-18', 'F', '6777-8888', 'ijimenez@email.com', 'Tocumen, Ciudad de Panamá', '8-888-9999', 'Cédula'),
(9, 'Fernando', 'Rojas Paz', '1980-04-05', 'M', '6777-9999', 'frojas@email.com', 'Alcalde Díaz, Ciudad de Panamá', '8-999-0000', 'Cédula'),
(10, 'Gabriela', 'Medina Torres', '1992-12-22', 'F', '6777-0000', 'gmedina@email.com', 'Costa del Este, Ciudad de Panamá', '8-000-1111', 'Cédula');

SET IDENTITY_INSERT HospitalOne.Clientes OFF;

PRINT '============================================';
PRINT 'Inserción de datos completada exitosamente';
PRINT 'Total de registros insertados:';
PRINT '  - Especialidades: 8';
PRINT '  - Doctores: 8';
PRINT '  - Consultorios: 8';
PRINT '  - Clientes: 10';
PRINT '============================================';
