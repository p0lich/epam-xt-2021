SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_GetByNameAndPassword]
	@name int,
	@password nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1
		Id, Name, DateOfBirth, Password, IsAdmin
		FROM Users
		WHERE Name = @name AND Password = @password
END
GO
