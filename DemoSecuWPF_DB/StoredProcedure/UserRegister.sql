CREATE PROCEDURE [dbo].[UserRegister]
	@email VARCHAR(50),
	@password VARCHAR(50),
	@nickname VARCHAR(50)
AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @secretKey VARCHAR(200)
  SET @secretKey = dbo.GetSecretKey()

  DECLARE @salt VARCHAR(100)
  SET @salt = CONCAT(NEWID(), NEWID(), NEWID())

  DECLARE @password_hash VARBINARY(64)
  SET @password_hash = HASHBYTES('SHA2_512', CONCAT(@salt, @secretKey, @password, @salt))

  INSERT INTO [Users] (Email, [Password], Nickname, Salt) 
  OUTPUT inserted.Id
  VALUES (@email, @password_hash, @nickname, @salt)
END