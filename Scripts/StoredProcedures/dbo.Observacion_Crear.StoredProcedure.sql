USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Observacion_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Observacion_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Observacion_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Observacion_Crear
--======================================================
CREATE PROCEDURE [dbo].[Observacion_Crear]
@Descripcion varchar(50),
@TipoObservacionID int,
@UsuarioCreacionID int,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Observacion (
		Descripcion,
		TipoObservacionID,
		UsuarioCreacionID,
		FechaCreacion,
		Activo
	)
	VALUES(
		@Descripcion,
		@TipoObservacionID,
		@UsuarioCreacionID,
		GETDATE(),
		@Activo
	)
	SET NOCOUNT OFF;
END

GO
