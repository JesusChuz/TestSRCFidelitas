use SIST_RECONOCIMIENTO;
use SIST_RECONOCIMIENTO;

SELECT * FROM AspNetRoleClaims;
SELECT * FROM AspNetRoles;
SELECT * FROM AspNetUserClaims;
SELECT * FROM AspNetUserLogins;
SELECT * FROM AspNetUserRoles;
SELECT * FROM Engineers;
SELECT Email, LockoutEnabled FROM AspNetUsers;
SELECT * FROM __EFMigrationsHistory;

--Delete from AspNetUsers
--Delete from AspNetUserRoles

SELECT * FROM CSAT;
SELECT * FROM Phrases;
SELECT * FROM Engineers;
SELECT * FROM Log_PasswordUpdate;
SELECT * FROM Positions;
SELECT * FROM Rewards;
DELETE FROM Rewards where ID_Reward = 4;
SELECT * FROM Purchases where Reward_ID = 4;
DELETE FROM Purchases where Reward_ID = 4;
SELECT * FROM Manager;

SELECT * FROM Recognitions;
SELECT * FROM Rol;


DELETE FROM Recognitions
DELETE FROM Positions
DELETE FROM Engineers

DELETE FROM AspNetUsers where Email = 'john@gmail.com';