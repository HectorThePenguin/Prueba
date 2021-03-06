USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_ObtenerCamionIDProveedorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Vigilancia_ObtenerCamionIDProveedorID]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_ObtenerCamionIDProveedorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Eduardo Cota
-- Create date: 19/05/2014
-- Description:  Comprueba y trae placa seleccionada
-- Vigilancia_ObtenerCamionIDProveedorID 43, 375
-- =============================================
CREATE PROCEDURE [dbo].[Vigilancia_ObtenerCamionIDProveedorID]
@CamionID INT,
@ProveedorID INT
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT CamionID,
			 PlacaCamion,
			 Activo
      FROM Camion
     WHERE (CamionID = @CamionID) 
	   AND (ProveedorID = @ProveedorID)
	  SET NOCOUNT OFF;
  END

GO
