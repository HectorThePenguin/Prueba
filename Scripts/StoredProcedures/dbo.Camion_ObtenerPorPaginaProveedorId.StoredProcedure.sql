USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorPaginaProveedorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Camion_ObtenerPorPaginaProveedorId]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorPaginaProveedorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 06/01/2014
-- Description:  Obtener listado de Camiones 
-- Camion_ObtenerPorPaginaProveedorId 1
-- =============================================
CREATE PROCEDURE [dbo].[Camion_ObtenerPorPaginaProveedorId]
(
	@PlacaCamion VARCHAR(10)
	, @ProveedorID INT
	, @Inicio INT
	, @Limite INT
)
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT ROW_NUMBER() OVER (ORDER BY PlacaCamion ASC) AS RowNum,
			CamionID,
			ProveedorID,
			PlacaCamion,
			Activo
	  INTO #Datos
      FROM Camion
      WHERE PlacaCamion LIKE '%' + @PlacaCamion + '%'
		AND ProveedorID = @ProveedorID
	  SELECT
			CamionID,
			ProveedorID,
			PlacaCamion,
			Activo
		FROM #Datos
		WHERE RowNum BETWEEN @Inicio AND @Limite
		SELECT 
			COUNT(CamionID) AS TotalReg
		From #Datos
		DROP TABLE #Datos
      SET NOCOUNT OFF;
  END

GO
