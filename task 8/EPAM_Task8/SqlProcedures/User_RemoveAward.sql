SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_RemoveAward]
	@id_user int,
	@id_award int
AS
BEGIN
	SET NOCOUNT OFF;

	DELETE FROM Users_Awards
	WHERE Id_User = @id_user AND Id_Award = @id_award
END
GO
