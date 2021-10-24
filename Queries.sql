--Create Sample User Table

CREATE TABLE [UserInfoTable]
(
	Id INT Primary Key IDENTITY(1,1),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	UserName NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(50) NOT NULL,
	Gender NVARCHAR(10) NOT NULL,
)

--DROP TABLE UserInfoTable

Insert into UserInfoTable VALUES
('Andrew','Simmons','andrew_simmons','test','Male'),
('Jessica','Park','jessi_park','test','Female'),
('Dorothy','Logan','dorothy_logan','test','Other'),
('Michael','Carey','michael_carry','test','Male'),
('Helen','Keller','hellen_keller','test','Female')

select * from UserInfoTable

--Create Sample Interests Table

--Create Sample Stored Procedure to Insert into User Table

CREATE OR ALTER PROCEDURE sp_GetAllUsers
AS 
	BEGIN
		Select Id, FirstName, LastName, Gender
		from UserInfoTable
	END

--EXEC sp_GetAllUsers

--Create Sample Stored Procedure to Retrieve from User Table based on Id

CREATE OR ALTER PROCEDURE sp_GetSpecificUser 
(@UserId INT)
AS 
	BEGIN
		Select Id, FirstName, LastName, Gender
		from UserInfoTable Where Id = @UserId
	END

--EXEC sp_GetSpecificUser 3

CREATE OR ALTER PROCEDURE sp_CreateNewUser
(
@FName nvarchar(50),
@LName nvarchar(50),
@UserName nvarchar(50),
@pass nvarchar(50),
@gender nvarchar(10)
)
AS
	BEGIN			
		Insert into UserInfoTable Values
		(@FName,@LName,@UserName,@pass,@gender)

		SELECT IDENT_CURRENT('UserInfoTable') as UserId
	END

--EXEC sp_CreateNewUser 'V','N','v_n','test','Male'
--Select * From UserInfoTable

CREATE OR ALTER PROCEDURE sp_UpdateUser
(
@FName nvarchar(50),
@LName nvarchar(50),
@UserName nvarchar(50),
@pass nvarchar(50),
@gender nvarchar(10),
@UserId Int
)
AS
	BEGIN			
		Update UserInfoTable
		set FirstName = @FName, LastName = @LName, UserName = @UserName,
		[Password] = @pass, Gender = @gender
		where Id = @UserId

		SELECT @UserId as UserID
	END

--EXEC sp_UpdateUser 'Vanguard','N','v_n','test','Female',8

CREATE OR ALTER PROCEDURE sp_DeleteUser
(
@UserId Int
)
AS
	BEGIN			
		Delete from UserInfoTable
		where Id = @UserId

		Select 'Success' as [Response]
	END

--Exec sp_DeleteUser 9
--select * from UserInfoTable