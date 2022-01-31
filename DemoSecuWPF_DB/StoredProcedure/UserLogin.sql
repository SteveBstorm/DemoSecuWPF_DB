CREATE PROCEDURE [dbo].[UserLogin]
	@email VARCHAR(50),
	@password VARCHAR(50)
AS
BEGIN
	DECLARE @secretKey VARCHAR(200)
	SET @secretKey = dbo.GetSecretKey()

	DECLARE @salt VARCHAR(100)
	SET @salt = (SELECT Salt FROM [Users] WHERE Email = @email)

	DECLARE @password_hash VARBINARY(64)
	SET @password_hash = HASHBYTES('SHA2_512', CONCAT(@salt, @secretKey, @password, @salt))

	SELECT Id, Email, Nickname FROM [Users] WHERE Email = @email AND [Password] = @password_hash

END