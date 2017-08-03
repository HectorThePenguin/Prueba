USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Retencion_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Retencion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Retencion_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
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
CREATE PROCEDURE [dbo].[Retencion_ObtenerPorID]
@RetencionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		RetencionID,
		Descripcion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID	
	FROM Retencion
	WHERE RetencionID = @RetencionID
	SET NOCOUNT OFF;
END

GO
