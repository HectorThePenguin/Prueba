USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoOrganizacion_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoOrganizacion_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TipoOrganizacion_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 27/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoOrganizacion_Crear
--======================================================
CREATE PROCEDURE [dbo].[TipoOrganizacion_Crear]
@TipoProcesoID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TipoOrganizacion (
		TipoProcesoID,
		Descripcion,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@TipoProcesoID,
		@Descripcion,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
