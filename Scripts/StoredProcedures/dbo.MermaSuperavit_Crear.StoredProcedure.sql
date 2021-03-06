USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MermaSuperavit_Crear]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 13/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : MermaSuperavit_Crear
--======================================================
CREATE PROCEDURE [dbo].[MermaSuperavit_Crear]
@AlmacenID int,
@ProductoID int,
@Merma decimal(12,2),
@Superavit decimal(12,2),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT MermaSuperavit (
		AlmacenID,
		ProductoID,
		Merma,
		Superavit,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@AlmacenID,
		@ProductoID,
		@Merma,
		@Superavit,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
