USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoAlmacen_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoAlmacen_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TipoAlmacen_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoAlmacen_Crear
--======================================================
CREATE PROCEDURE [dbo].[TipoAlmacen_Crear]
@Descripcion varchar(50),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TipoAlmacen (
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
