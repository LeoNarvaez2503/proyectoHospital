-- ============================================================
-- SCRIPT DE DATOS SEMILLA (SEEDS) - BDHospital
-- Inserta datos iniciales de prueba para todos los módulos
-- ============================================================

USE BDHospital;
GO

-- 1. ESPECIALIDADES SEMILLA
IF NOT EXISTS (SELECT 1 FROM Especialidad WHERE Nombre = 'Cardiología')
    INSERT INTO Especialidad (Nombre) VALUES ('Cardiología');

IF NOT EXISTS (SELECT 1 FROM Especialidad WHERE Nombre = 'Pediatría')
    INSERT INTO Especialidad (Nombre) VALUES ('Pediatría');

IF NOT EXISTS (SELECT 1 FROM Especialidad WHERE Nombre = 'Neurología')
    INSERT INTO Especialidad (Nombre) VALUES ('Neurología');

IF NOT EXISTS (SELECT 1 FROM Especialidad WHERE Nombre = 'Dermatología')
    INSERT INTO Especialidad (Nombre) VALUES ('Dermatología');

IF NOT EXISTS (SELECT 1 FROM Especialidad WHERE Nombre = 'Ginecología')
    INSERT INTO Especialidad (Nombre) VALUES ('Ginecología');

IF NOT EXISTS (SELECT 1 FROM Especialidad WHERE Nombre = 'Traumatología')
    INSERT INTO Especialidad (Nombre) VALUES ('Traumatología');

IF NOT EXISTS (SELECT 1 FROM Especialidad WHERE Nombre = 'Medicina General')
    INSERT INTO Especialidad (Nombre) VALUES ('Medicina General');
GO

-- 2. PACIENTES SEMILLA
IF NOT EXISTS (SELECT 1 FROM Paciente WHERE Email = 'juan.perez@email.com')
    INSERT INTO Paciente (Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion) 
    VALUES ('Juan', 'Pérez', '1985-04-12', '0991234567', 'juan.perez@email.com', 'Av. Amazonas N24-101 y Orellana');

IF NOT EXISTS (SELECT 1 FROM Paciente WHERE Email = 'maria.rodriguez@email.com')
    INSERT INTO Paciente (Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion) 
    VALUES ('María', 'Rodríguez', '1992-08-25', '0998765432', 'maria.rodriguez@email.com', 'Calle Larga 456 y Huaynacápac');

IF NOT EXISTS (SELECT 1 FROM Paciente WHERE Email = 'luis.fernandez@email.com')
    INSERT INTO Paciente (Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion) 
    VALUES ('Luis', 'Fernández', '1978-11-03', '0995551234', 'luis.fernandez@email.com', 'Av. 10 de Agosto y Colón');

IF NOT EXISTS (SELECT 1 FROM Paciente WHERE Email = 'sofia.benitez@email.com')
    INSERT INTO Paciente (Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion) 
    VALUES ('Sofía', 'Benítez', '2001-02-14', '0994448888', 'sofia.benitez@email.com', 'Calle Guayaquil 789 y Olmedo');

IF NOT EXISTS (SELECT 1 FROM Paciente WHERE Email = 'carlos.andrade@email.com')
    INSERT INTO Paciente (Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion) 
    VALUES ('Carlos', 'Andrade', '1965-06-30', '0993332211', 'carlos.andrade@email.com', 'Av. De los Shyris N32-45');
GO

-- 3. MÉDICOS SEMILLA
IF NOT EXISTS (SELECT 1 FROM Medico WHERE Email = 'carlos.mendoza@hospital.com')
    INSERT INTO Medico (Nombre, Apellido, EspecialidadId, Telefono, Email) 
    VALUES ('Dr. Carlos', 'Mendoza', (SELECT TOP 1 Id FROM Especialidad WHERE Nombre = 'Cardiología'), '0981112233', 'carlos.mendoza@hospital.com');

IF NOT EXISTS (SELECT 1 FROM Medico WHERE Email = 'ana.lopez@hospital.com')
    INSERT INTO Medico (Nombre, Apellido, EspecialidadId, Telefono, Email) 
    VALUES ('Dra. Ana', 'López', (SELECT TOP 1 Id FROM Especialidad WHERE Nombre = 'Pediatría'), '0984445566', 'ana.lopez@hospital.com');

IF NOT EXISTS (SELECT 1 FROM Medico WHERE Email = 'roberto.gomez@hospital.com')
    INSERT INTO Medico (Nombre, Apellido, EspecialidadId, Telefono, Email) 
    VALUES ('Dr. Roberto', 'Gómez', (SELECT TOP 1 Id FROM Especialidad WHERE Nombre = 'Neurología'), '0987778899', 'roberto.gomez@hospital.com');

IF NOT EXISTS (SELECT 1 FROM Medico WHERE Email = 'maria.torres@hospital.com')
    INSERT INTO Medico (Nombre, Apellido, EspecialidadId, Telefono, Email) 
    VALUES ('Dra. María', 'Torres', (SELECT TOP 1 Id FROM Especialidad WHERE Nombre = 'Dermatología'), '0982223344', 'maria.torres@hospital.com');

IF NOT EXISTS (SELECT 1 FROM Medico WHERE Email = 'fernando.ruiz@hospital.com')
    INSERT INTO Medico (Nombre, Apellido, EspecialidadId, Telefono, Email) 
    VALUES ('Dr. Fernando', 'Ruiz', (SELECT TOP 1 Id FROM Especialidad WHERE Nombre = 'Medicina General'), '0985556677', 'fernando.ruiz@hospital.com');
GO

-- 4. CITAS SEMILLA
IF NOT EXISTS (SELECT 1 FROM Cita WHERE idPaciente = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'juan.perez@email.com'))
    INSERT INTO Cita (idPaciente, idMedico, fecha, estado) 
    VALUES (
        (SELECT TOP 1 Id FROM Paciente WHERE Email = 'juan.perez@email.com'), 
        (SELECT TOP 1 Id FROM Medico WHERE Email = 'carlos.mendoza@hospital.com'), 
        '2026-08-01 09:00:00', 'Pendiente'
    );

IF NOT EXISTS (SELECT 1 FROM Cita WHERE idPaciente = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'maria.rodriguez@email.com'))
    INSERT INTO Cita (idPaciente, idMedico, fecha, estado) 
    VALUES (
        (SELECT TOP 1 Id FROM Paciente WHERE Email = 'maria.rodriguez@email.com'), 
        (SELECT TOP 1 Id FROM Medico WHERE Email = 'ana.lopez@hospital.com'), 
        '2026-08-01 10:30:00', 'Confirmada'
    );

IF NOT EXISTS (SELECT 1 FROM Cita WHERE idPaciente = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'luis.fernandez@email.com'))
    INSERT INTO Cita (idPaciente, idMedico, fecha, estado) 
    VALUES (
        (SELECT TOP 1 Id FROM Paciente WHERE Email = 'luis.fernandez@email.com'), 
        (SELECT TOP 1 Id FROM Medico WHERE Email = 'roberto.gomez@hospital.com'), 
        '2026-08-02 11:00:00', 'Completada'
    );

IF NOT EXISTS (SELECT 1 FROM Cita WHERE idPaciente = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'sofia.benitez@email.com'))
    INSERT INTO Cita (idPaciente, idMedico, fecha, estado) 
    VALUES (
        (SELECT TOP 1 Id FROM Paciente WHERE Email = 'sofia.benitez@email.com'), 
        (SELECT TOP 1 Id FROM Medico WHERE Email = 'maria.torres@hospital.com'), 
        '2026-08-03 15:00:00', 'Pendiente'
    );

IF NOT EXISTS (SELECT 1 FROM Cita WHERE idPaciente = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'carlos.andrade@email.com'))
    INSERT INTO Cita (idPaciente, idMedico, fecha, estado) 
    VALUES (
        (SELECT TOP 1 Id FROM Paciente WHERE Email = 'carlos.andrade@email.com'), 
        (SELECT TOP 1 Id FROM Medico WHERE Email = 'fernando.ruiz@hospital.com'), 
        '2026-08-04 16:30:00', 'Cancelada'
    );
GO

-- 5. TRATAMIENTOS SEMILLA
IF NOT EXISTS (SELECT 1 FROM Tratamiento WHERE PacienteId = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'juan.perez@email.com'))
    INSERT INTO Tratamiento (PacienteId, Descripcion, Fecha, Costo) 
    VALUES ((SELECT TOP 1 Id FROM Paciente WHERE Email = 'juan.perez@email.com'), 'Electrocardiograma y chequeo preventivo de hipertensión arterial', '2026-07-20 09:30:00', 150.00);

IF NOT EXISTS (SELECT 1 FROM Tratamiento WHERE PacienteId = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'maria.rodriguez@email.com'))
    INSERT INTO Tratamiento (PacienteId, Descripcion, Fecha, Costo) 
    VALUES ((SELECT TOP 1 Id FROM Paciente WHERE Email = 'maria.rodriguez@email.com'), 'Tratamiento pediátrico para bronquitis leve y nebulización', '2026-07-21 11:00:00', 85.50);

IF NOT EXISTS (SELECT 1 FROM Tratamiento WHERE PacienteId = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'luis.fernandez@email.com'))
    INSERT INTO Tratamiento (PacienteId, Descripcion, Fecha, Costo) 
    VALUES ((SELECT TOP 1 Id FROM Paciente WHERE Email = 'luis.fernandez@email.com'), 'Resonancia magnética cerebral y terapia de rehabilitación física', '2026-07-22 14:15:00', 320.00);

IF NOT EXISTS (SELECT 1 FROM Tratamiento WHERE PacienteId = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'sofia.benitez@email.com'))
    INSERT INTO Tratamiento (PacienteId, Descripcion, Fecha, Costo) 
    VALUES ((SELECT TOP 1 Id FROM Paciente WHERE Email = 'sofia.benitez@email.com'), 'Tratamiento dermatológico para acné y limpieza facial médica', '2026-07-23 16:00:00', 110.00);

IF NOT EXISTS (SELECT 1 FROM Tratamiento WHERE PacienteId = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'carlos.andrade@email.com'))
    INSERT INTO Tratamiento (PacienteId, Descripcion, Fecha, Costo) 
    VALUES ((SELECT TOP 1 Id FROM Paciente WHERE Email = 'carlos.andrade@email.com'), 'Consulta médica general y perfil lipídico completo de laboratorio', '2026-07-24 10:00:00', 75.00);
GO

-- 6. FACTURACIÓN SEMILLA
IF NOT EXISTS (SELECT 1 FROM Facturacion WHERE PacienteId = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'juan.perez@email.com'))
    INSERT INTO Facturacion (PacienteId, Monto, MetodoPago, FechaPago) 
    VALUES ((SELECT TOP 1 Id FROM Paciente WHERE Email = 'juan.perez@email.com'), 150.00, 'Tarjeta de Crédito', '2026-07-20 10:00:00');

IF NOT EXISTS (SELECT 1 FROM Facturacion WHERE PacienteId = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'maria.rodriguez@email.com'))
    INSERT INTO Facturacion (PacienteId, Monto, MetodoPago, FechaPago) 
    VALUES ((SELECT TOP 1 Id FROM Paciente WHERE Email = 'maria.rodriguez@email.com'), 85.50, 'Efectivo', '2026-07-21 11:30:00');

IF NOT EXISTS (SELECT 1 FROM Facturacion WHERE PacienteId = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'luis.fernandez@email.com'))
    INSERT INTO Facturacion (PacienteId, Monto, MetodoPago, FechaPago) 
    VALUES ((SELECT TOP 1 Id FROM Paciente WHERE Email = 'luis.fernandez@email.com'), 320.00, 'Transferencia Bancaria', '2026-07-22 15:00:00');

IF NOT EXISTS (SELECT 1 FROM Facturacion WHERE PacienteId = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'sofia.benitez@email.com'))
    INSERT INTO Facturacion (PacienteId, Monto, MetodoPago, FechaPago) 
    VALUES ((SELECT TOP 1 Id FROM Paciente WHERE Email = 'sofia.benitez@email.com'), 110.00, 'Tarjeta de Débito', '2026-07-23 16:30:00');

IF NOT EXISTS (SELECT 1 FROM Facturacion WHERE PacienteId = (SELECT TOP 1 Id FROM Paciente WHERE Email = 'carlos.andrade@email.com'))
    INSERT INTO Facturacion (PacienteId, Monto, MetodoPago, FechaPago) 
    VALUES ((SELECT TOP 1 Id FROM Paciente WHERE Email = 'carlos.andrade@email.com'), 75.00, 'Efectivo', '2026-07-24 10:30:00');
GO
