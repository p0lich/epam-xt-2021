USE [UsersAwardsDb]
GO
/****** Object:  StoredProcedure [dbo].[User_GetAwards]    Script Date: 04.07.2021 9:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[User_GetAwards]
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id, Title FROM Users_Awards INNER JOIN Awards
	ON Id_User = @id AND Id_Award = Awards.Id
END
