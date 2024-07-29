-- Active: 1722202155824@@bfyc53lgxvksghqnywvp-mysql.services.clever-cloud.com@3306@bfyc53lgxvksghqnywvp
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

CREATE TABLE Role (
    RoleID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE User (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(100) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    RoleID INT,
    FOREIGN KEY (RoleID) REFERENCES Role(RoleID)
);

INSERT INTO Role (Name) VALUES
('Admin'),
('Cliente');

INSERT INTO Users (Email, Password, RoleID) VALUES 
('prueba.121@gmail.com', 'prueba', 1); -- Contraseña en texto plano





RENAME TABLE User TO Users;

RENAME TABLE Role TO Roles;

-- Cambiar el nombre de la columna PasswordHash a Password en la tabla Users
ALTER TABLE Users CHANGE COLUMN UserName Email VARCHAR(255) NOT NULL;
