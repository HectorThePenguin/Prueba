USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CondicionJaula_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CondicionJaula_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CondicionJaula_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 22/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CondicionJaula_Crear
--======================================================
CREATE PROCEDURE [dbo].[CondicionJaula_Crear]
@Descripcion varchar(255),
@Activo bit, 
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CondicionJaula (
		Descripcion,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Descripcion,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
