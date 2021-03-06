USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorProveedorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Camion_ObtenerPorProveedorID]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorProveedorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtener listado de Camiones por su ProveedorID
-- Camion_ObtenerPorProveedorID 4857
-- =============================================
CREATE PROCEDURE [dbo].[Camion_ObtenerPorProveedorID]
(
	@ProveedorID int
)
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT CamionID
			,ProveedorID
			,PlacaCamion
			,Activo
			,FechaCreacion
			,UsuarioCreacionID
			,FechaModificacion
			,UsuarioModificacionID
			,NumEconomico
      FROM Camion
		WHERE ProveedorID = @ProveedorID
		AND Activo = 1
		ORDER BY PlacaCamion
      SET NOCOUNT OFF;
  END

GO
