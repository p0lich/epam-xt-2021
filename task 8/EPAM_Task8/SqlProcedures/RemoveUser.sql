USE [UsersAwardsDb]
GO
/****** Object:  StoredProcedure [dbo].[RemoveUser]    Script Date: 04.07.2021 8:17:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RemoveUser]
	@id int
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE FROM Users
	WHERE Id = @id
END
