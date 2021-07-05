SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_GetByName]
	@name nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1
		Id, Name, DateOfBirth, Password, IsAdmin
		FROM Users
		WHERE Name = @name
END
GO
