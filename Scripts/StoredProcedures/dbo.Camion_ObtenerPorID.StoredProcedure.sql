USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Camion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtener listado de Camiones 
-- Camion_ObtenerPorID 1
-- =============================================
CREATE PROCEDURE [dbo].[Camion_ObtenerPorID]
(
	@CamionID int
)
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT CamionID,
			ProveedorID,
			PlacaCamion,
			Activo
      FROM Camion
      WHERE CamionID = @CamionID
      SET NOCOUNT OFF;
  END

GO
