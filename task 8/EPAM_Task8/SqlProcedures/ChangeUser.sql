SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ChangeUser]
	@id int,
	@name nvarchar(50),
	@dateOfBirth date,
	@password nvarchar(50),
	@isAdmin bit
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Users SET
	Name = @name,
	DateOfBirth = @dateOfBirth,
	Password = @password,
	IsAdmin = @isAdmin
	WHERE Id = @id
END
GO
