USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerClasificacionGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_ObtenerClasificacionGanado]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerClasificacionGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Cesar Valdez
-- Create date: 2013/12/11
-- Description: Sp para obtener las clasificaciones de los ganados
-- Origen     : APInterfaces
-- EXEC [dbo].[CorteGanado_ObtenerClasificacionGanado]
--=============================================
CREATE PROCEDURE [dbo].[CorteGanado_ObtenerClasificacionGanado]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ClasificacionGanadoID,
		   Descripcion,
		   Activo,
		   FechaCreacion,
		   UsuarioCreacionID,
		   FechaModificacion,
		   UsuarioModificacionID
	  FROM ClasificacionGanado
	 WHERE Activo = 1
  ORDER BY Descripcion ASC
	SET NOCOUNT OFF;
END

GO
