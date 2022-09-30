
-- Create Database
CREATE DATABASE UserRegistrationTask;

-- Create USER table
CREATE TABLE UserRegistrationTask.dbo.[USER] (
	ID int IDENTITY(1,1) NOT NULL,
	UserName varchar(100) NOT NULL,
	Password varchar(100) NOT NULL,
	Email varchar(100) NOT NULL,
	MobileNo int NOT NULL,
	Gender varchar(100) NOT NULL,
	Age int NOT NULL,
	CONSTRAINT USER_PK PRIMARY KEY (ID)
);

-- Create Procedures
CREATE PROCEDURE DBO.REGISTER_USER
(
    @UserName VARCHAR(100),
    @Password VARCHAR(100),
    @Email VARCHAR(100),
    @MobileNo INT ,
    @Gender VARCHAR(100),
    @Age INT
)

AS
BEGIN
	
	INSERT INTO dbo.[USER] (UserName, Password, Email, MobileNo,Gender,Age)
	VALUES (@UserName,@Password,@Email,@MobileNo,@Gender,@Age)
END

CREATE PROCEDURE DBO.VALIDATE_USER
(
    @UserName VARCHAR(100),
    @Password VARCHAR(100)
)
AS 
BEGIN
	SET NOCOUNT ON;
	SELECT  * FROM dbo.[USER] WHERE UserName=@UserName  and Password=@Password
End