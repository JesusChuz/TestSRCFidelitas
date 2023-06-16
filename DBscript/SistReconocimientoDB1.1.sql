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

<<<<<<< HEAD
CREATE PROCEDURE ShowPoints
    @email nvarchar(256)
AS
BEGIN
	SELECT e.Points
	FROM Engineers e, AspNetUsers u
	WHERE u.Email = @email AND e.ID_Account = u.Id;
END

drop procedure ShowPoints
SELECT e.Points
FROM Engineers e, AspNetUsers u
WHERE u.Email = 'JOEL.JIMENEZ@GMAIL.COM' AND e.ID_Account = u.Id;

EXEC ShowPoints @email='MELANY.QUESADA@GMAIL.COM';

CREATE PROCEDURE ShowIdEngineer
    @email nvarchar(256)
AS
BEGIN
	SELECT e.ID_Engineer
	FROM Engineers e, AspNetUsers u
	WHERE u.Email = @email AND e.ID_Account = u.Id;
END

EXEC ShowIdEngineer @email='J.MENDEZQUESADA18@GMAIL.COM';

CREATE PROCEDURE UpdatePoints
    @Engineer_ID int,
    @newPoints int
AS
BEGIN
    UPDATE Engineers
    SET Points = @newPoints
    WHERE ID_Engineer = @Engineer_ID;
END

EXEC UpdatePoints @Engineer_ID = 10, @newPoints = 10000;

CREATE PROCEDURE SelectMyManager
    @ID_Engineer int,
	@Reward_ID int,
	@ID_Purchase int
AS
BEGIN
	SELECT DISTINCT m.Email, m.Name_Manager, u.Email as EmailE, e.Name_Engineer, CONVERT(VARCHAR(255),e.Points) AS Points, r.Reward_Name, CONVERT(VARCHAR(255),r.Price) AS Price
	FROM Manager m, Engineers e, AspNetUsers u, Rewards r, Purchases p
	WHERE m.ID_Manager = e.ID_Manager AND e.ID_Engineer = @ID_Engineer AND u.Id = e.ID_Account AND r.ID_Reward = @Reward_ID AND p.ID_Purchase = @ID_Purchase;
END

EXEC SelectMyManager @ID_Engineer = 10, @Reward_ID = 4, @ID_Purchase = 6
=======
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
