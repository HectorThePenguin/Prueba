USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Producto_ObtenerPorPagina 0, '', 3, 0, 0, 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorPagina]
@ProductoID INT,
@Descripcion VARCHAR(50),
@FamiliaID INT,
@SubFamiliaID INT,
@UnidadID INT,
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY P.Descripcion ASC) AS RowNum,
		P.ProductoID,
		P.Descripcion,
		P.SubFamiliaID,
		P.UnidadID,
		P.Activo,
		SB.FamiliaID,
		SB.Descripcion AS DescripcionSubFamilia,
		F.Descripcion AS DescripcionFamilia,
		UM.Descripcion AS DescripcionUnidad,
		p.ManejaLote,
	    p.MaterialSAP
	INTO #Datos
	FROM Producto P
	INNER JOIN SubFamilia SB
		ON (P.SubFamiliaID = SB.SubFamiliaID
			AND @SubFamiliaID IN (SB.SubFamiliaID, 0)
			AND @FamiliaID IN (SB.FamiliaID, 0))
	INNER JOIN Familia F
		ON (SB.FamiliaID = F.FamiliaID)
	INNER JOIN UnidadMedicion UM
		ON (P.UnidadID = UM.UnidadID
			AND @UnidadID IN (P.UnidadID, 0))			
	WHERE (P.Descripcion LIKE '%' + @Descripcion + '%' OR @Descripcion = '' )
			AND @ProductoID IN (P.ProductoID, 0)
			AND P.Activo = @Activo

	SELECT
		ProductoID,
		Descripcion,
		SubFamiliaID,
		UnidadID,
		Activo,
		FamiliaID,
		DescripcionSubFamilia,
		DescripcionFamilia,
		DescripcionUnidad,
		ManejaLote,
		MaterialSAP
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite

	SELECT
	COUNT(ProductoID) AS [TotalReg]
	FROM #Datos

	DROP TABLE #Datos

	SET NOCOUNT OFF;
END

GO
