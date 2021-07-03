SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateUser]
	@name nvarchar(50),
	@dateOfBirth date,
	@password nvarchar(50)
AS
	INSERT INTO dbo.Users (Name, DateOfBirth, Password)
	VALUES (@name, @dateOfBirth, @password)
GO
