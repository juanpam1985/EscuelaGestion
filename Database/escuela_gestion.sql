CREATE DATABASE EscuelaGestionDB;
GO

USE EscuelaGestionDB;
GO

CREATE TABLE Estudiantes (
    estudiante_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(80) NOT NULL,
    apellido VARCHAR(80) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
    grado VARCHAR(30) NOT NULL
);
GO

CREATE TABLE Profesores (
    profesor_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(80) NOT NULL,
    apellido VARCHAR(80) NOT NULL,
    especialidad VARCHAR(80) NOT NULL,
    email VARCHAR(120) NOT NULL
);
GO

CREATE TABLE Clases (
    clase_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    horario VARCHAR(60) NOT NULL,
    profesor_id INT NOT NULL,
    CONSTRAINT FK_Clases_Profesores
        FOREIGN KEY (profesor_id) REFERENCES Profesores(profesor_id)
);
GO

CREATE TABLE AsignacionesClases (
    asignacion_id INT IDENTITY(1,1) PRIMARY KEY,
    estudiante_id INT NOT NULL,
    clase_id INT NOT NULL,
    fecha_asignacion DATE NOT NULL,
    CONSTRAINT FK_Asignaciones_Estudiantes
        FOREIGN KEY (estudiante_id) REFERENCES Estudiantes(estudiante_id),
    CONSTRAINT FK_Asignaciones_Clases
        FOREIGN KEY (clase_id) REFERENCES Clases(clase_id)
);
GO

INSERT INTO Estudiantes (nombre, apellido, fecha_nacimiento, grado) VALUES
('Mateo', 'Guaman', '2011-04-15', 'Octavo'),
('Camila', 'Revelo', '2010-09-20', 'Noveno'),
('Daniel', 'Morales', '2012-01-08', 'Septimo');
GO

INSERT INTO Profesores (nombre, apellido, especialidad, email) VALUES
('Luis', 'Perez', 'Matematicas', 'luis.perez@escuela.edu'),
('Ana', 'Torres', 'Lengua y Literatura', 'ana.torres@escuela.edu'),
('Carlos', 'Mena', 'Ciencias Naturales', 'carlos.mena@escuela.edu');
GO

INSERT INTO Clases (nombre, horario, profesor_id) VALUES
('Matematicas Basica', 'Lunes 08:00 - 10:00', 1),
('Lengua y Literatura', 'Martes 10:00 - 12:00', 2),
('Ciencias Naturales', 'Miercoles 09:00 - 11:00', 3);
GO

INSERT INTO AsignacionesClases (estudiante_id, clase_id, fecha_asignacion) VALUES
(1, 1, '2026-01-10'),
(2, 2, '2026-01-10'),
(3, 3, '2026-01-11'),
(1, 2, '2026-01-12');
GO

SELECT * FROM Estudiantes;
SELECT * FROM Profesores;
SELECT * FROM Clases;
SELECT * FROM AsignacionesClases;
GO
