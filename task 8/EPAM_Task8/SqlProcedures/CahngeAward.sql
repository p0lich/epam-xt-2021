SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ChangeAward]
	@id int,
	@title nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Awards SET
	Title = @title
	WHERE Id = @id
END
GO
