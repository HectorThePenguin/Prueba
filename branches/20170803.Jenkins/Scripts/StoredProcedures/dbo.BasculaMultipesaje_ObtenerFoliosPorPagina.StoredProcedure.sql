USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[BasculaMultipesaje_ObtenerFoliosPorPagina]     ******/
DROP PROCEDURE [dbo].[BasculaMultipesaje_ObtenerFoliosPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[BasculaMultipesaje_ObtenerFoliosPorPagina]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pablo Bórquez 
-- Create date: 01/12/2015
-- Description: Obtiene los folios de Báscula Multipesaje por página
-- BasculaMultipesaje_ObtenerFoliosPorPagina '2016/12/06', 1, 15, 1, 'Pedro'
-- =============================================  
CREATE PROCEDURE [dbo].[BasculaMultipesaje_ObtenerFoliosPorPagina]
	@Fecha DATE,
	@Inicio INT,         
	@Limite INT,
	@OrganizacionId INT,
	@Descripcion VARCHAR(100)  
AS
BEGIN
	SET NOCOUNT ON;  

	SELECT 
		ROW_NUMBER() OVER ( ORDER BY b.Folio ASC) AS RowNum,    
		b.Folio,
		b.Chofer,
		b.Producto
	INTO
		#BasculaMultipesaje
	FROM
		BasculaMultipesaje b
	WHERE
		DAY(Fecha) = DAY(@Fecha) AND
		MONTH(Fecha) = MONTH(@Fecha) AND
		YEAR(Fecha) = YEAR(@Fecha) AND
		OrganizacionId = @OrganizacionId AND
		Chofer like concat('%',@Descripcion,'%') AND
		b.Activo = 1
	SELECT 
		b.Folio,
		b.Chofer,
		b.Producto
	FROM 
		#BasculaMultipesaje b
	WHERE
		RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(Folio)AS TotalReg 
	FROM #BasculaMultipesaje	
	DROP TABLE #BasculaMultipesaje

	SET NOCOUNT OFF;  
END