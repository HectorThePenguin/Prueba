USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoTratamiento_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoTratamiento_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TipoTratamiento_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoTratamiento_Crear
--======================================================
CREATE PROCEDURE [dbo].[TipoTratamiento_Crear]
@Descripcion varchar(50),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TipoTratamiento (
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
	SET NOCOUNT OFF;
END

GO
