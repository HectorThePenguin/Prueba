USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorProveedorIDCamionID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Camion_ObtenerPorProveedorIDCamionID]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorProveedorIDCamionID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jesus Alvarez
-- Create date: 25/06/2014
-- Description:  Obtener camion por camionid y proveedorid
-- Camion_ObtenerPorProveedorIDCamionID 1
-- =============================================
CREATE PROCEDURE [dbo].[Camion_ObtenerPorProveedorIDCamionID]
@ProveedorID INT,
@CamionID INT
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT CamionID,
			ProveedorID,
			PlacaCamion,
			Activo
      FROM Camion
      WHERE ProveedorID = @ProveedorID
		AND CamionID = @CamionID
      SET NOCOUNT OFF;
  END

GO
