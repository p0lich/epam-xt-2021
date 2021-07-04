SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Award_GetUsers]
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id, Name, DateOfBirth, Password, IsAdmin FROM Users_Awards INNER JOIN Users
	ON Id_Award = @id AND Id_User = Users.Id
END
GO
