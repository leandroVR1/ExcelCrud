INSERT INTO Universidad (Nombre) VALUES
 ('Universidad de Costa Rica'),
 ('Universidad Central de Venezuela'),
 ('Universidad Autónoma de Puerto Rico');

 INSERT INTO Carrera (Nombre) VALUES
 ('Ingeniería Civil'),
 ('Licenciatura en Administración'),
 ('Licenciatura en Economía'),
 ('Ingeniería en Sistemas');

 INSERT INTO Decano (Nombre) VALUES
 ('Juan Pablo Montoya'),
 ('María Antonia García'),
 ('Daniela Gómez');

 INSERT INTO Profesor (Nombre, Apellido, Correo, Telefono) VALUES
 ('Juan Carlos', 'Perez', 'jperez@example.com', '123456'),
 ('Maria', 'Lopez', 'lopez@example.com', '654321'),
 ('Pedro', 'Garcia', 'garcia@example.com', '9876543');

 INSERT INTO Materia (Nombre) VALUES
 ('Matemáticas'),
 ('Química'),
 ('Física'),
 ('Lenguaje'),
 ('Biología'),
 ('Inglés');

 INSERT INTO Estudiante (Nombre, Apellido, Correo, Telefono) VALUES
 ('Juan', 'Perez', 'jperez@example.com', '123456'),
 ('Maria', 'Lopez', 'lopez@example.com', '6543221'),
 ('Pedro', 'Garcia', 'garcia@example.com', '912345');

INSERT INTO Inscripcion (EstudianteID, MateriaID, ProfesorID, DecanoID, UniversidadID, CarreraID, Semestre, Año, EstadoDeInscripcion) VALUES
(1, 1, 1, 1, 1, 1, 1, 2024, 'Inscrito'),
(1, 2, 1, 1, 1, 1, 1, 2024, 'Inscrito'),
(2, 3, 2, 2, 2, 2, 2, 2024, 'Inscrito'),
(2, 4, 2, 2, 2, 2, 2, 2024, 'Inscrito'),
(3, 5, 3, 3, 3, 3, 3, 2024, 'Inscrito'),
(3, 6, 3, 3, 3, 3, 3, 2024, 'Inscrito'),
(1, 3, 1, 1, 1, 2, 1, 2024, 'Inscrito'),
(2, 5, 2, 2, 2, 1, 2, 2024, 'Inscrito'),
(3, 2, 3, 3, 3, 1, 3, 2024, 'Inscrito'),
(1, 4, 1, 1, 1, 1, 1, 2024, 'Inscrito');