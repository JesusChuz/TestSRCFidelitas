CREATE DATABASE SIST_RECONOCIMIENTO;
USE SIST_RECONOCIMIENTO;
----------------------------------------------------------
-- Creación de la tabla "Rol"
CREATE TABLE Rol (
    ID_Rol INT PRIMARY KEY,
    Nombre_Rol VARCHAR(50)
);

-- Creación de la tabla "Posiciones"
CREATE TABLE Posiciones (
    ID_Posicion INT PRIMARY KEY,
    Nombre_Posicion VARCHAR(50)
);

-- Creación de la tabla "Recompensas"
CREATE TABLE Recompensas (
    ID_Recompensa INT PRIMARY KEY,
    Nombre_Recompensa VARCHAR(100),
    Descripcion_Recompensa VARCHAR(1000),
    Precio INTEGER,
    Imagen VARBINARY(MAX)
);

-- Creación de la tabla "Ingeniero"
CREATE TABLE Ingeniero (
    ID_Ingeniero INT PRIMARY KEY,
    Email VARCHAR(50),
    Nombre VARCHAR(50),
    Apellido VARCHAR(50),
    Rol INT,
    Posicion INT,
    Puntos INT,
    Password VARCHAR(500),
    FOREIGN KEY (Rol) REFERENCES Rol(ID_Rol),
    FOREIGN KEY (Posicion) REFERENCES Posiciones(ID_Posicion)
);

-- Creación de la tabla "Log_CambioPassword"
CREATE TABLE Log_CambioPassword (
    ID_Cambio INT PRIMARY KEY,
    Razon VARCHAR(50),
    ID_Ingeniero INT,
    FechaCambio DATETIME,
    FOREIGN KEY (ID_Ingeniero) REFERENCES Ingeniero(ID_Ingeniero)
);

-- Creación de la tabla "Reconocimientos"
CREATE TABLE Reconocimientos (
    ID_Reconocimiento INT PRIMARY KEY,
    Solicitante INT,
    Ing_Reconocido INT,
    Estado VARCHAR(50),
    Numero_Caso VARCHAR(30),
    Comentario VARCHAR(500),
	Administrador INT,
    FOREIGN KEY (Solicitante) REFERENCES Ingeniero(ID_Ingeniero),
    FOREIGN KEY (Ing_Reconocido) REFERENCES Ingeniero(ID_Ingeniero),
	FOREIGN KEY (Administrador) REFERENCES Ingeniero(ID_Ingeniero)
);

-- Creación de la tabla "Canjeos"
CREATE TABLE Canjeos (
    ID_Canjeo INT PRIMARY KEY,
    ID_Ingeniero INT,
    ID_Recompensa INT,
    PrecioRecompensa INT,
    FOREIGN KEY (ID_Ingeniero) REFERENCES Ingeniero(ID_Ingeniero),
    FOREIGN KEY (ID_Recompensa) REFERENCES Recompensas(ID_Recompensa)
);

-- Creación de la tabla "CSAT"
CREATE TABLE CSAT (
    ID_Survey INT PRIMARY KEY,
    Puntaje INT,
    Comentarios VARCHAR(500),
    Email_Ingeniero VARCHAR(50),
    DTC INT,
	ID_Ingeniero INT,
	FOREIGN KEY (ID_Ingeniero) REFERENCES Ingeniero(ID_Ingeniero),
);

-- Creación de la tabla "Frases"
CREATE TABLE Frases (
    ID_Frase INT PRIMARY KEY,
    Frase VARCHAR(500),
    ID_Ingeniero INT,
    FOREIGN KEY (ID_Ingeniero) REFERENCES Ingeniero(ID_Ingeniero)
);

--Stored Procedures
CREATE PROCEDURE GetLockoutEnabledAndIsNew
    @Email VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT LockoutEnabled, IsNew
    FROM AspNetUsers
    WHERE Email = @Email;
END;

EXEC GetLockoutEnabledAndIsNew @Email = 'john.wick@gmail.com';

