USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListCorral_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListCorral_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CheckListCorral_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListCorral_Crear
--======================================================
CREATE PROCEDURE [dbo].[CheckListCorral_Crear]
@OrganizacionID INT,
@LoteID int,
@PDF varbinary(max),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CheckListCorral (
		OrganizacionID,
		LoteID,
		PDF,
		Activo,
		UsuarioCreacionID,
		FechaCreacion		
	)
	VALUES(
		@OrganizacionID ,
		@LoteID,
		@PDF,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()		
	)
	SET NOCOUNT OFF;
END

GO
