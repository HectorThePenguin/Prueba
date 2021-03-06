USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorPaginaTenganProgramacionFletesInterna]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorPaginaTenganProgramacionFletesInterna]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorPaginaTenganProgramacionFletesInterna]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 21/07/2014
-- Description: 
-- SpName     : Producto_ObtenerPorPaginaTenganProgramacionFletesInterna 0, '', 0, 0, 0, 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorPaginaTenganProgramacionFletesInterna]
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
		DISTINCT
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
	INNER JOIN FleteInterno FT
		ON (P.ProductoID = FT.ProductoID 
		AND FT.Activo = @Activo)	
	WHERE (P.Descripcion LIKE '%' + @Descripcion + '%' OR @Descripcion = '' )
			AND @ProductoID IN (P.ProductoID, 0)
			AND P.Activo = @Activo

	SELECT 
		ROW_NUMBER() OVER (ORDER BY D.Descripcion ASC) AS RowNum,
		D.ProductoID,
		D.Descripcion,
		D.SubFamiliaID,
		D.UnidadID,
		D.Activo,
		D.FamiliaID,
		D.DescripcionSubFamilia,
		D.DescripcionFamilia,
		D.DescripcionUnidad,
		D.ManejaLote,
		D.MaterialSAP
	INTO #Datos2
	FROM #Datos D

	SELECT 
		D2.ProductoID,
		D2.Descripcion,
		D2.SubFamiliaID,
		D2.UnidadID,
		D2.Activo,
		D2.FamiliaID,
		D2.DescripcionSubFamilia,
		D2.DescripcionFamilia,
		D2.DescripcionUnidad,
		D2.ManejaLote,
		D2.MaterialSAP
	FROM #Datos2 D2
	WHERE RowNum BETWEEN @Inicio AND @Limite

	SELECT
	COUNT(ProductoID) AS [TotalReg]
	FROM #Datos2

	DROP TABLE #Datos
	DROP TABLE #Datos2

	SET NOCOUNT OFF;
END

GO
