CREATE DATABASE SIST_RECONOCIMIENTO;
USE SIST_RECONOCIMIENTO;
----------------------------------------------------------
-- Creación de la tabla "Posiciones"
CREATE TABLE Positions (
    ID_Position INT PRIMARY KEY,
    Position_Name VARCHAR(50)
);

-- Creación de la tabla "Recompensas"
CREATE TABLE Rewards (
    ID_Reward INT PRIMARY KEY,
    Reward_Name VARCHAR(100),
    Reward_Description VARCHAR(1000),
    Price INTEGER,
    Picture VARBINARY(MAX)
);
-- Creación de la tabla "Ingeniero"
CREATE TABLE Engineers (
    ID_Engineer INT PRIMARY KEY,
    Name_Engineer VARCHAR(50),
    LastName_Engineer VARCHAR(50),
    Position INT,
    Points INT,
    Picture VARBINARY(MAX),
	ID_Account NVARCHAR(450),
    FOREIGN KEY (Position) REFERENCES Positions(ID_Position),
	FOREIGN KEY (ID_Account) REFERENCES AspNetUsers(Id)
);

-- Creación de la tabla "Log_CambioPassword"
CREATE TABLE Log_PasswordUpdate (
    ID_Change INT PRIMARY KEY,
    Reason VARCHAR(50),
    ID_Engineer INT,
    Change_Date DATETIME,
    FOREIGN KEY (ID_Engineer) REFERENCES Engineers(ID_Engineer)
);

-- Creación de la tabla "Reconocimientos"
CREATE TABLE Recognitions (
    ID_Recognition INT PRIMARY KEY,
    Petitioner_Eng INT,
    Recognized_Eng INT,
    Recognition_State VARCHAR(50),
    Case_Number VARCHAR(30),
    Comment VARCHAR(500),
	Evaluator_Admin INT,
    FOREIGN KEY (Petitioner_Eng) REFERENCES Engineers(ID_Engineer),
    FOREIGN KEY (Recognized_Eng) REFERENCES Engineers(ID_Engineer),
	FOREIGN KEY (Evaluator_Admin) REFERENCES Engineers(ID_Engineer)
);

-- Creación de la tabla "Canjeos"
CREATE TABLE Purchases (
    ID_Purchase INT PRIMARY KEY,
    Engineer_ID INT,
    Reward_ID INT,
    Reward_Price INT,
    FOREIGN KEY (Engineer_ID) REFERENCES Engineers(ID_Engineer),
    FOREIGN KEY (Reward_ID) REFERENCES Rewards(ID_Reward)
);

-- Creación de la tabla "CSAT"
CREATE TABLE CSAT (
    ID_Survey INT PRIMARY KEY,
    Score INT,
    Comments VARCHAR(500),
    Email_Engineer VARCHAR(50),
    DTC INT,
	Engineer_ID INT,
	FOREIGN KEY (Engineer_ID) REFERENCES Engineers(ID_Engineer),
);

-- Creación de la tabla "Frases"
CREATE TABLE Phrases (
    Phrases_ID INT PRIMARY KEY,
    Phrase VARCHAR(500),
    Engineer_ID INT,
    FOREIGN KEY (Engineer_ID) REFERENCES Engineers(ID_Engineer)
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

