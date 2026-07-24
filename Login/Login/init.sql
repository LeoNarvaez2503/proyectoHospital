-- ============================================================
-- Script de inicializacion: BDHospital
-- Crea la base de datos, tablas, foreign keys y stored procedures
-- ============================================================

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BDHospital')
BEGIN
    CREATE DATABASE BDHospital
END
GO

USE BDHospital
GO

-- ============================================================
-- TABLAS
-- ============================================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Especialidad')
BEGIN
    CREATE TABLE Especialidad (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nombre NVARCHAR(100) NOT NULL
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Paciente')
BEGIN
    CREATE TABLE Paciente (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nombre NVARCHAR(100) NOT NULL,
        Apellido NVARCHAR(100) NOT NULL,
        FechaNacimiento DATETIME NOT NULL,
        Telefono NVARCHAR(20) NULL,
        Email NVARCHAR(100) NULL,
        Direccion NVARCHAR(200) NULL
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Medico')
BEGIN
    CREATE TABLE Medico (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nombre NVARCHAR(100) NOT NULL,
        Apellido NVARCHAR(100) NOT NULL,
        EspecialidadId INT NOT NULL,
        Telefono NVARCHAR(20) NULL,
        Email NVARCHAR(100) NULL,
        CONSTRAINT FK_Medico_Especialidad FOREIGN KEY (EspecialidadId) REFERENCES Especialidad(Id)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
BEGIN
    CREATE TABLE Usuario (
        idUsuario INT IDENTITY(1,1) PRIMARY KEY,
        correo NVARCHAR(100) NOT NULL,
        clave NVARCHAR(200) NOT NULL,
        rol NVARCHAR(50) NOT NULL DEFAULT 'Secretario'
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Cita')
BEGIN
    CREATE TABLE Cita (
        idCita INT IDENTITY(1,1) PRIMARY KEY,
        idPaciente INT NOT NULL,
        idMedico INT NOT NULL,
        fecha DATETIME NOT NULL,
        estado NVARCHAR(50) NOT NULL,
        CONSTRAINT FK_Cita_Paciente FOREIGN KEY (idPaciente) REFERENCES Paciente(Id),
        CONSTRAINT FK_Cita_Medico FOREIGN KEY (idMedico) REFERENCES Medico(Id)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tratamiento')
BEGIN
    CREATE TABLE Tratamiento (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        PacienteId INT NOT NULL,
        Descripcion NVARCHAR(500) NOT NULL,
        Fecha DATETIME NOT NULL,
        Costo DECIMAL(10,2) NOT NULL,
        CONSTRAINT FK_Tratamiento_Paciente FOREIGN KEY (PacienteId) REFERENCES Paciente(Id)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Facturacion')
BEGIN
    CREATE TABLE Facturacion (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        PacienteId INT NOT NULL,
        Monto DECIMAL(10,2) NOT NULL,
        MetodoPago NVARCHAR(50) NOT NULL,
        FechaPago DATETIME NOT NULL,
        CONSTRAINT FK_Facturacion_Paciente FOREIGN KEY (PacienteId) REFERENCES Paciente(Id)
    )
END
GO

-- ============================================================
-- STORED PROCEDURES: Usuario
-- ============================================================

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_RegistrarUsuario')
    DROP PROCEDURE sp_RegistrarUsuario
GO

CREATE PROCEDURE sp_RegistrarUsuario
    @correo NVARCHAR(100),
    @clave NVARCHAR(200),
    @Registrado BIT OUTPUT,
    @Mensaje NVARCHAR(100) OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Usuario WHERE correo = @correo)
    BEGIN
        SET @Registrado = 0
        SET @Mensaje = 'El correo ya esta registrado'
    END
    ELSE
    BEGIN
        INSERT INTO Usuario (correo, clave, rol) VALUES (@correo, @clave, 'Secretario')
        SET @Registrado = 1
        SET @Mensaje = 'Usuario registrado exitosamente'
    END
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_ValidarUsuario')
    DROP PROCEDURE sp_ValidarUsuario
GO

CREATE PROCEDURE sp_ValidarUsuario
    @correo NVARCHAR(100),
    @clave NVARCHAR(200)
AS
BEGIN
    SELECT idUsuario, rol FROM Usuario WHERE correo = @correo AND clave = @clave
END
GO

-- ============================================================
-- STORED PROCEDURES: Paciente
-- ============================================================

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspListarPacientes')
    DROP PROCEDURE uspListarPacientes
GO

CREATE PROCEDURE uspListarPacientes
AS
BEGIN
    SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion
    FROM Paciente
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspRecuperarPacientes')
    DROP PROCEDURE uspRecuperarPacientes
GO

CREATE PROCEDURE uspRecuperarPacientes
    @id INT
AS
BEGIN
    SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion
    FROM Paciente
    WHERE Id = @id
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspGuardarPacientes')
    DROP PROCEDURE uspGuardarPacientes
GO

CREATE PROCEDURE uspGuardarPacientes
    @id INT,
    @nombre NVARCHAR(100),
    @apellido NVARCHAR(100),
    @fechaNacimiento DATETIME,
    @telefono NVARCHAR(20),
    @email NVARCHAR(100),
    @direccion NVARCHAR(200)
AS
BEGIN
    IF @id = 0
    BEGIN
        INSERT INTO Paciente (Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion)
        VALUES (@nombre, @apellido, @fechaNacimiento, @telefono, @email, @direccion)
    END
    ELSE
    BEGIN
        UPDATE Paciente
        SET Nombre = @nombre,
            Apellido = @apellido,
            FechaNacimiento = @fechaNacimiento,
            Telefono = @telefono,
            Email = @email,
            Direccion = @direccion
        WHERE Id = @id
    END
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspEliminarPaciente')
    DROP PROCEDURE uspEliminarPaciente
GO

CREATE PROCEDURE uspEliminarPaciente
    @id INT
AS
BEGIN
    DELETE FROM Paciente WHERE Id = @id
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspFiltrarPacientes')
    DROP PROCEDURE uspFiltrarPacientes
GO

CREATE PROCEDURE uspFiltrarPacientes
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @FechaNacimiento DATETIME,
    @Telefono NVARCHAR(20),
    @Email NVARCHAR(100),
    @Direccion NVARCHAR(200)
AS
BEGIN
    SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion
    FROM Paciente
    WHERE (@Nombre = '' OR Nombre LIKE '%' + @Nombre + '%')
      AND (@Apellido = '' OR Apellido LIKE '%' + @Apellido + '%')
      AND (@Telefono = '' OR Telefono LIKE '%' + @Telefono + '%')
      AND (@Email = '' OR Email LIKE '%' + @Email + '%')
      AND (@Direccion = '' OR Direccion LIKE '%' + @Direccion + '%')
END
GO

-- ============================================================
-- STORED PROCEDURES: Medico
-- ============================================================

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspListarMedicos')
    DROP PROCEDURE uspListarMedicos
GO

CREATE PROCEDURE uspListarMedicos
AS
BEGIN
    SELECT Id, Nombre, Apellido, EspecialidadId, Telefono, Email
    FROM Medico
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspRecuperarMedicos')
    DROP PROCEDURE uspRecuperarMedicos
GO

CREATE PROCEDURE uspRecuperarMedicos
    @id INT
AS
BEGIN
    SELECT Id, Nombre, Apellido, EspecialidadId, Telefono, Email
    FROM Medico
    WHERE Id = @id
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspGuardarMedicos')
    DROP PROCEDURE uspGuardarMedicos
GO

CREATE PROCEDURE uspGuardarMedicos
    @id INT,
    @nombre NVARCHAR(100),
    @apellido NVARCHAR(100),
    @especialidadId INT,
    @telefono NVARCHAR(20),
    @email NVARCHAR(100)
AS
BEGIN
    IF @id = 0
    BEGIN
        INSERT INTO Medico (Nombre, Apellido, EspecialidadId, Telefono, Email)
        VALUES (@nombre, @apellido, @especialidadId, @telefono, @email)
    END
    ELSE
    BEGIN
        UPDATE Medico
        SET Nombre = @nombre,
            Apellido = @apellido,
            EspecialidadId = @especialidadId,
            Telefono = @telefono,
            Email = @email
        WHERE Id = @id
    END
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspEliminarMedico')
    DROP PROCEDURE uspEliminarMedico
GO

CREATE PROCEDURE uspEliminarMedico
    @id INT
AS
BEGIN
    DELETE FROM Medico WHERE Id = @id
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspFiltrarMedicos')
    DROP PROCEDURE uspFiltrarMedicos
GO

CREATE PROCEDURE uspFiltrarMedicos
    @nombre NVARCHAR(100),
    @apellido NVARCHAR(100),
    @especialidadId INT,
    @telefono NVARCHAR(20),
    @email NVARCHAR(100)
AS
BEGIN
    SELECT Id, Nombre, Apellido, EspecialidadId, Telefono, Email
    FROM Medico
    WHERE (@nombre = '' OR Nombre LIKE '%' + @nombre + '%')
      AND (@apellido = '' OR Apellido LIKE '%' + @apellido + '%')
      AND (@especialidadId = 0 OR EspecialidadId = @especialidadId)
      AND (@telefono = '' OR Telefono LIKE '%' + @telefono + '%')
      AND (@email = '' OR Email LIKE '%' + @email + '%')
END
GO

-- ============================================================
-- STORED PROCEDURES: Especialidad
-- ============================================================

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspListarEspecialidades')
    DROP PROCEDURE uspListarEspecialidades
GO

CREATE PROCEDURE uspListarEspecialidades
AS
BEGIN
    SELECT Id, Nombre
    FROM Especialidad
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspRecuperarEspecialidades')
    DROP PROCEDURE uspRecuperarEspecialidades
GO

CREATE PROCEDURE uspRecuperarEspecialidades
    @id INT
AS
BEGIN
    SELECT Id, Nombre
    FROM Especialidad
    WHERE Id = @id
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspGuardarEspecialidades')
    DROP PROCEDURE uspGuardarEspecialidades
GO

CREATE PROCEDURE uspGuardarEspecialidades
    @id INT,
    @nombre NVARCHAR(100)
AS
BEGIN
    IF @id = 0
    BEGIN
        INSERT INTO Especialidad (Nombre) VALUES (@nombre)
    END
    ELSE
    BEGIN
        UPDATE Especialidad SET Nombre = @nombre WHERE Id = @id
    END
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspEliminarEspecialidad')
    DROP PROCEDURE uspEliminarEspecialidad
GO

CREATE PROCEDURE uspEliminarEspecialidad
    @id INT
AS
BEGIN
    DELETE FROM Especialidad WHERE Id = @id
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspFiltrarEspecialidades')
    DROP PROCEDURE uspFiltrarEspecialidades
GO

CREATE PROCEDURE uspFiltrarEspecialidades
    @nombre NVARCHAR(100)
AS
BEGIN
    SELECT Id, Nombre
    FROM Especialidad
    WHERE (@nombre = '' OR Nombre LIKE '%' + @nombre + '%')
END
GO

-- ============================================================
-- STORED PROCEDURES: Cita
-- ============================================================

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspListarCitas')
    DROP PROCEDURE uspListarCitas
GO

CREATE PROCEDURE uspListarCitas
AS
BEGIN
    SELECT idCita, idPaciente, idMedico, fecha, estado
    FROM Cita
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspRecuperarCitas')
    DROP PROCEDURE uspRecuperarCitas
GO

CREATE PROCEDURE uspRecuperarCitas
    @id INT
AS
BEGIN
    SELECT idCita, idPaciente, idMedico, fecha, estado
    FROM Cita
    WHERE idCita = @id
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspGuardarCitas')
    DROP PROCEDURE uspGuardarCitas
GO

CREATE PROCEDURE uspGuardarCitas
    @id INT,
    @PacienteId INT,
    @MedicoID INT,
    @FechaHora DATETIME,
    @Estado NVARCHAR(50)
AS
BEGIN
    IF @id = 0
    BEGIN
        INSERT INTO Cita (idPaciente, idMedico, fecha, estado)
        VALUES (@PacienteId, @MedicoID, @FechaHora, @Estado)
    END
    ELSE
    BEGIN
        UPDATE Cita
        SET idPaciente = @PacienteId,
            idMedico = @MedicoID,
            fecha = @FechaHora,
            estado = @Estado
        WHERE idCita = @id
    END
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspEliminarCita')
    DROP PROCEDURE uspEliminarCita
GO

CREATE PROCEDURE uspEliminarCita
    @id INT
AS
BEGIN
    DELETE FROM Cita WHERE idCita = @id
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspFiltrarCitas')
    DROP PROCEDURE uspFiltrarCitas
GO

CREATE PROCEDURE uspFiltrarCitas
    @PacienteId INT,
    @MedicoID INT,
    @FechaHora DATETIME,
    @Estado NVARCHAR(50)
AS
BEGIN
    SELECT idCita, idPaciente, idMedico, fecha, estado
    FROM Cita
    WHERE (@PacienteId = 0 OR idPaciente = @PacienteId)
      AND (@MedicoID = 0 OR idMedico = @MedicoID)
      AND (@Estado = '' OR estado LIKE '%' + @Estado + '%')
END
GO

-- ============================================================
-- STORED PROCEDURES: Tratamiento
-- ============================================================

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspListarTratamientos')
    DROP PROCEDURE uspListarTratamientos
GO

CREATE PROCEDURE uspListarTratamientos
AS
BEGIN
    SELECT Id, PacienteId, Descripcion, Fecha, Costo
    FROM Tratamiento
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspRecuperarTratamientos')
    DROP PROCEDURE uspRecuperarTratamientos
GO

CREATE PROCEDURE uspRecuperarTratamientos
    @id INT
AS
BEGIN
    SELECT Id, PacienteId, Descripcion, Fecha, Costo
    FROM Tratamiento
    WHERE Id = @id
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspGuardarTratamientos')
    DROP PROCEDURE uspGuardarTratamientos
GO

CREATE PROCEDURE uspGuardarTratamientos
    @id INT,
    @pacienteId INT,
    @descripcion NVARCHAR(500),
    @fecha DATETIME,
    @costo DECIMAL(10,2)
AS
BEGIN
    IF @id = 0
    BEGIN
        INSERT INTO Tratamiento (PacienteId, Descripcion, Fecha, Costo)
        VALUES (@pacienteId, @descripcion, @fecha, @costo)
    END
    ELSE
    BEGIN
        UPDATE Tratamiento
        SET PacienteId = @pacienteId,
            Descripcion = @descripcion,
            Fecha = @fecha,
            Costo = @costo
        WHERE Id = @id
    END
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspEliminarTratamiento')
    DROP PROCEDURE uspEliminarTratamiento
GO

CREATE PROCEDURE uspEliminarTratamiento
    @id INT
AS
BEGIN
    DELETE FROM Tratamiento WHERE Id = @id
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspFiltrarTratamientos')
    DROP PROCEDURE uspFiltrarTratamientos
GO

CREATE PROCEDURE uspFiltrarTratamientos
    @pacienteId INT,
    @descripcion NVARCHAR(500),
    @fecha DATETIME
AS
BEGIN
    SELECT Id, PacienteId, Descripcion, Fecha, Costo
    FROM Tratamiento
    WHERE (@pacienteId = 0 OR PacienteId = @pacienteId)
      AND (@descripcion = '' OR Descripcion LIKE '%' + @descripcion + '%')
END
GO

-- ============================================================
-- STORED PROCEDURES: Facturacion
-- ============================================================

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspListarFacturacion')
    DROP PROCEDURE uspListarFacturacion
GO

CREATE PROCEDURE uspListarFacturacion
AS
BEGIN
    SELECT Id, PacienteId, Monto, MetodoPago, FechaPago
    FROM Facturacion
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspRecuperarFacturacion')
    DROP PROCEDURE uspRecuperarFacturacion
GO

CREATE PROCEDURE uspRecuperarFacturacion
    @id INT
AS
BEGIN
    SELECT Id, PacienteId, Monto, MetodoPago, FechaPago
    FROM Facturacion
    WHERE Id = @id
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspGuardarFacturacion')
    DROP PROCEDURE uspGuardarFacturacion
GO

CREATE PROCEDURE uspGuardarFacturacion
    @id INT,
    @pacienteId INT,
    @monto DECIMAL(10,2),
    @metodoPago NVARCHAR(50),
    @fechaPago DATETIME
AS
BEGIN
    IF @id = 0
    BEGIN
        INSERT INTO Facturacion (PacienteId, Monto, MetodoPago, FechaPago)
        VALUES (@pacienteId, @monto, @metodoPago, @fechaPago)
    END
    ELSE
    BEGIN
        UPDATE Facturacion
        SET PacienteId = @pacienteId,
            Monto = @monto,
            MetodoPago = @metodoPago,
            FechaPago = @fechaPago
        WHERE Id = @id
    END
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspEliminarFacturacion')
    DROP PROCEDURE uspEliminarFacturacion
GO

CREATE PROCEDURE uspEliminarFacturacion
    @id INT
AS
BEGIN
    DELETE FROM Facturacion WHERE Id = @id
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'uspFiltrarFacturacion')
    DROP PROCEDURE uspFiltrarFacturacion
GO

CREATE PROCEDURE uspFiltrarFacturacion
    @pacienteId INT,
    @monto DECIMAL(10,2),
    @metodoPago NVARCHAR(50),
    @fechaPago DATETIME
AS
BEGIN
    SELECT Id, PacienteId, Monto, MetodoPago, FechaPago
    FROM Facturacion
    WHERE (@pacienteId = 0 OR PacienteId = @pacienteId)
      AND (@metodoPago = '' OR MetodoPago LIKE '%' + @metodoPago + '%')
END
GO
