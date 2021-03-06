USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Retencion_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Retencion_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Retencion_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/12/02
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[Retencion_Crear]
@Descripcion VARCHAR(50),
	@Activo BIT,
	@FechaCreacion SMALLDATETIME,
	@UsuarioCreacionID INT,
	@FechaModificacion SMALLDATETIME,
	@UsuarioModificacionID INT	
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Retencion(
		Descripcion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID	
	)
	VALUES(
		@Descripcion,
		@Activo,
		@FechaCreacion,
		@UsuarioCreacionID,
		@FechaModificacion,
		@UsuarioModificacionID	
	)
	SET NOCOUNT OFF;
END

GO
