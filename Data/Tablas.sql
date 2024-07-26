-- Active: 1721953868099@@bfyc53lgxvksghqnywvp-mysql.services.clever-cloud.com@3306@bfyc53lgxvksghqnywvp
CREATE TABLE Estudiante (
    EstudianteID INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    Apellido VARCHAR(100),
    Correo VARCHAR(100),
    Telefono VARCHAR(20)
);

CREATE TABLE Materia (
    MateriaID INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100)
);

CREATE TABLE Profesor (
    ProfesorID INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    Apellido VARCHAR(100),
    Correo VARCHAR(100),
    Telefono VARCHAR(20)
);

CREATE TABLE Decano (
    DecanoID INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100)
);

CREATE TABLE Universidad (
    UniversidadID INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100)
);

CREATE TABLE Carrera (
    CarreraID INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100)
);

CREATE TABLE Inscripcion (
    InscripcionID INT AUTO_INCREMENT PRIMARY KEY,
    EstudianteID INT,
    MateriaID INT,
    ProfesorID INT,
    DecanoID INT,
    UniversidadID INT,
    CarreraID INT,
    Semestre INT,
    Año INT,
    EstadoDeInscripcion VARCHAR(50),
    FOREIGN KEY (EstudianteID) REFERENCES Estudiante(EstudianteID),
    FOREIGN KEY (MateriaID) REFERENCES Materia(MateriaID),
    FOREIGN KEY (ProfesorID) REFERENCES Profesor(ProfesorID),
    FOREIGN KEY (DecanoID) REFERENCES Decano(DecanoID),
    FOREIGN KEY (UniversidadID) REFERENCES Universidad(UniversidadID),
    FOREIGN KEY (CarreraID) REFERENCES Carrera(CarreraID)
);







