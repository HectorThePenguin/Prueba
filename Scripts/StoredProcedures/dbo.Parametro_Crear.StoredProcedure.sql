USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Parametro_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: crea un registro de Parametro
-- SpName     : Parametro_Crear
--======================================================
CREATE PROCEDURE [dbo].[Parametro_Crear]
@Descripcion varchar(50),
@TipoParametroID INT,
@Clave varchar(30),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Parametro (
		TipoParametroID,
		Descripcion,
		Clave,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@TipoParametroID,
		@Descripcion,
		@Clave,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
