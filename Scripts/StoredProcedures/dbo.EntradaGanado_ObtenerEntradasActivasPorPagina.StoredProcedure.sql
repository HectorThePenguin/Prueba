USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradasActivasPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerEntradasActivasPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradasActivasPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 05-11-2013
-- Description:	Obtener Entradas de ganado Activas por pagina 
-- EntradaGanado_ObtenerEntradasActivasPorPagina 1,1,10
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerEntradasActivasPorPagina]
	@OrganizacionID INT, 
	@Inicio INT, 
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		ROW_NUMBER() OVER ( ORDER BY FolioEntrada ASC) AS RowNum,
		FolioEntrada, 
		EntradaGanadoID
	INTO #EntradaGanado
	FROM EntradaGanado 
	WHERE OrganizacionID = @OrganizacionID
	AND Activo = 1
	AND PesoBruto != 0
	AND PesoTara != 0
	SELECT 
		FolioEntrada, 
		EntradaGanadoID
	FROM #EntradaGanado	
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(EntradaGanadoID)AS TotalReg 
	FROM #EntradaGanado	
	DROP TABLE #EntradaGanado		   
END

GO
