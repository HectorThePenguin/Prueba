USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoObjetivoCalidad_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoObjetivoCalidad_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TipoObjetivoCalidad_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoObjetivoCalidad_Crear
--======================================================
CREATE PROCEDURE [dbo].[TipoObjetivoCalidad_Crear]
@Descripcion varchar(50),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TipoObjetivoCalidad (
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
