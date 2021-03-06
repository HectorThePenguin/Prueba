USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Retencion_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Retencion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Retencion_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
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
CREATE PROCEDURE [dbo].[Retencion_Actualizar]
	@RetencionID INT,
	@Descripcion VARCHAR(50),
	@Activo BIT,
	@FechaCreacion SMALLDATETIME,
	@UsuarioCreacionID INT,
	@FechaModificacion SMALLDATETIME,
	@UsuarioModificacionID INT	
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE Retencion SET 
			Descripcion = @Descripcion,
			Activo = @Activo,
			FechaCreacion = @FechaCreacion,
			UsuarioCreacionID = @UsuarioCreacionID,
			FechaModificacion = @FechaModificacion,
			UsuarioModificacionID = @UsuarioModificacionID	
		WHERE RetencionID = @RetencionID
	SET NOCOUNT OFF;
END

GO
