USE [UsersAwardsDb]
GO
/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 03.07.2021 20:12:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CreateUser]
	@name nvarchar(50),
	@dateOfBirth date,
	@password nvarchar(50),
	@isAdmin bit
AS
	INSERT INTO dbo.Users (Name, DateOfBirth, Password, IsAdmin)
	VALUES (@name, @dateOfBirth, @password, @isAdmin)
