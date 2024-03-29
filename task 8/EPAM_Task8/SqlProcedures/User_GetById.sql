USE [UsersAwardsDb]
GO
/****** Object:  StoredProcedure [dbo].[User_GetById]    Script Date: 04.07.2021 8:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[User_GetById]
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1
		Id, Name, DateOfBirth, Password, IsAdmin
		FROM Users
		WHERE Id = @id
END
