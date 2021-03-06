USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoObservacion_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoObservacion_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TipoObservacion_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoObservacion_Crear
--======================================================
CREATE PROCEDURE [dbo].[TipoObservacion_Crear]
@Descripcion varchar(50),
@UsuarioCreacionID int,
@Activo	BIT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TipoObservacion (
		Descripcion,
		UsuarioCreacionID,
		FechaCreacion,
		Activo
	)
	VALUES(
		@Descripcion,
		@UsuarioCreacionID,
		GETDATE(),
		@Activo
	)
	SET NOCOUNT OFF;
END

GO
