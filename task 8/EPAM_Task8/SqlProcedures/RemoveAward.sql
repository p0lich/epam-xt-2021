USE [UsersAwardsDb]
GO
/****** Object:  StoredProcedure [dbo].[RemoveAward]    Script Date: 04.07.2021 8:47:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RemoveAward]
	@id int
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE FROM Awards
	WHERE Id = @id
END
