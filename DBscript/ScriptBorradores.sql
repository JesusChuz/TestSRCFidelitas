CREATE DATABASE SIST_RECONOCIMIENTO;
USE SIST_RECONOCIMIENTO;
----------------------------------------------------------
-- Creación de la tabla "Posiciones"
CREATE TABLE Positions (
    ID_Position INT PRIMARY KEY IDENTITY,
    Position_Name VARCHAR(50)
);

-- Creación de la tabla "Recompensas"
CREATE TABLE Rewards (
    ID_Reward INT PRIMARY KEY IDENTITY,
    Reward_Name VARCHAR(100),
    Reward_Description VARCHAR(1000),
    Price INTEGER,
    Picture VARBINARY(MAX),
);
-- Creación de la tabla "Ingeniero"
CREATE TABLE Engineers (
    ID_Engineer INT PRIMARY KEY IDENTITY,
    Name_Engineer VARCHAR(50),
    LastName_Engineer VARCHAR(50),
    Position INT,
    Points INT,
    Picture VARBINARY(MAX),
	ID_Account NVARCHAR(450),
    FOREIGN KEY (Position) REFERENCES Positions(ID_Position) ON DELETE NO ACTION ON UPDATE NO ACTION,
	FOREIGN KEY (ID_Account) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

-- Creación de la tabla "Log_CambioPassword"
CREATE TABLE Log_PasswordUpdate (
    ID_Change INT PRIMARY KEY IDENTITY,
    Reason VARCHAR(50),
    ID_Engineer INT,
    Change_Date DATETIME,
    FOREIGN KEY (ID_Engineer) REFERENCES Engineers(ID_Engineer) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Creación de la tabla "Reconocimientos"
CREATE TABLE Recognitions (
    ID_Recognition INT PRIMARY KEY IDENTITY,
    Petitioner_Eng INT,
    Recognized_Eng INT,
    Recognition_State VARCHAR(50),
    Case_Number VARCHAR(30),
    Comment VARCHAR(500),
	Evaluator_Admin INT,
    FOREIGN KEY (Petitioner_Eng) REFERENCES Engineers(ID_Engineer) ON DELETE NO ACTION ON UPDATE NO ACTION,
    FOREIGN KEY (Recognized_Eng) REFERENCES Engineers(ID_Engineer) ON DELETE NO ACTION ON UPDATE NO ACTION,
	FOREIGN KEY (Evaluator_Admin) REFERENCES Engineers(ID_Engineer) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Creación de la tabla "Canjeos"
CREATE TABLE Purchases (
    ID_Purchase INT PRIMARY KEY IDENTITY,
    Engineer_ID INT,
    Reward_ID INT,
    Reward_Price INT,
    FOREIGN KEY (Engineer_ID) REFERENCES Engineers(ID_Engineer) ON DELETE NO ACTION ON UPDATE NO ACTION,
    FOREIGN KEY (Reward_ID) REFERENCES Rewards(ID_Reward) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Creación de la tabla "CSAT"
CREATE TABLE CSAT (
    ID_Survey INT PRIMARY KEY IDENTITY,
    Score INT,
    Comments VARCHAR(500),
    Email_Engineer VARCHAR(50),
    DTC INT,
	Engineer_ID INT,
	FOREIGN KEY (Engineer_ID) REFERENCES Engineers(ID_Engineer) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Creación de la tabla "Frases"
CREATE TABLE Phrases (
    Phrases_ID INT PRIMARY KEY IDENTITY,
    Phrase VARCHAR(500),
    Engineer_ID INT,
    FOREIGN KEY (Engineer_ID) REFERENCES Engineers(ID_Engineer) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Eliminar una restricción de clave externa
ALTER TABLE Managers
DROP CONSTRAINT NombreRestriccion;


CREATE TABLE Manager (
    ID_Manager INT PRIMARY KEY IDENTITY,
    Name_Manager VARCHAR(50),
    LastName_Manager VARCHAR(50),
    Email VARCHAR(50)
);

ALTER TABLE Engineers
ADD CONSTRAINT FK_Manager
FOREIGN KEY (Manager_ID)
REFERENCES Manager (ID_Manager);

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

DROP PROCEDURE ChangeIsNew

CREATE PROCEDURE ChangeIsNew
    @email nvarchar(256),
    @newValue bit
AS
BEGIN
    UPDATE AspNetUsers
    SET IsNew = @newValue
    WHERE Email = @email;
	UPDATE AspNetUsers
	SET LockoutEnabled = @newValue
	WHERE Email = @email;
END

EXEC ChangeIsNew @email = 'jorge.granados@gmail.com', @newValue = 0;


CREATE PROCEDURE SelectManagers
AS
BEGIN
	SELECT * FROM MANAGER;
END

EXEC SelectManagers

CREATE PROCEDURE SelectPositions
AS
BEGIN
	SELECT * FROM Positions;
END

drop procedure SelectPositions

EXEC SelectPositions

select * from AspNetRoles

CREATE PROCEDURE SelectRoles
AS
BEGIN
	SELECT * FROM AspNetRoles;
END
EXEC SelectRoles

CREATE PROCEDURE SelectEngineers
AS
BEGIN

	SELECT U.UserName, U.Email, MG.Name_Manager AS Manager, R.Name AS RoleName, P.Position_Name AS Posicion
	FROM AspNetUsers U
	JOIN AspNetUserRoles UR ON U.Id = UR.UserId
	JOIN AspNetRoles R ON UR.RoleId = R.Id
	JOIN Engineers E ON U.Id = E.ID_Account
	JOIN Positions P ON E.Position = P.ID_Position
	JOIN Manager MG ON E.ID_Manager = MG.ID_Manager;

END

EXEC SelectEngineers

