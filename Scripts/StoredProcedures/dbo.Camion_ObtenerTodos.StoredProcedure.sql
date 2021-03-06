USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Camion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtener listado de Camiones
-- Camion_ObtenerTodos 1
-- =============================================
CREATE PROCEDURE [dbo].[Camion_ObtenerTodos]
@Activo BIT = null
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT CamionID,
			ProveedorID,
			PlacaCamion,
			Activo
      FROM Camion
      WHERE (Activo = @Activo OR @Activo is null)
	  ORDER BY PlacaCamion
      SET NOCOUNT OFF;
  END

GO
