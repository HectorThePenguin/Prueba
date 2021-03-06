USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorCamionIdProveedorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Camion_ObtenerPorCamionIdProveedorId]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorCamionIdProveedorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 06/01/2014
-- Description:  Obtener listado de Camiones 
-- Camion_ObtenerPorCamionIdProveedorId 1
-- =============================================
CREATE PROCEDURE [dbo].[Camion_ObtenerPorCamionIdProveedorId]
(
	@PlacaCamion VARCHAR(10)
	, @ProveedorID INT
)
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT CamionID,
			ProveedorID,
			PlacaCamion,
			Activo
      FROM Camion
      WHERE PlacaCamion = @PlacaCamion
		AND ProveedorID = @ProveedorID
      SET NOCOUNT OFF;
  END

GO
