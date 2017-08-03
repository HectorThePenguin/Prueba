USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoParametro_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoParametro_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TipoParametro_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: Crea un tipo de parametro
-- SpName     : TipoParametro_Crear
--======================================================
CREATE PROCEDURE [dbo].[TipoParametro_Crear]
	@Descripcion varchar(50),
	@Activo bit,
	@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TipoParametro (
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
