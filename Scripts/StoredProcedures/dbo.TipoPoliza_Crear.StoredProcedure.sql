USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoPoliza_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoPoliza_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TipoPoliza_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoPoliza_Crear 'prueba', '12', 1, 1 
--======================================================
CREATE PROCEDURE [dbo].[TipoPoliza_Crear]
@Descripcion varchar(50),
@ClavePoliza char(2),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TipoPoliza (
		Descripcion,
		ClavePoliza,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Descripcion,
		@ClavePoliza,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
