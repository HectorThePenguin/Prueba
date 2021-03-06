USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorPaginaFiltroFamiliaSubfamilias]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorPaginaFiltroFamiliaSubfamilias]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorPaginaFiltroFamiliaSubfamilias]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Producto_ObtenerPorPaginaFiltroFamiliaSubfamilias 0, '', 1, 6, 0, '', 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorPaginaFiltroFamiliaSubfamilias]
@ProductoID INT,
@Descripcion VARCHAR(50),
@FamiliaID INT,
@SubFamiliaID INT,
@UnidadID INT,
@ParametroDescripcion VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @SubProductos as TABLE
	(
		ProductoID INT primary key
	)
	DECLARE @Valor AS VARCHAR(255)

	SELECT @Valor = Valor 
	FROM ParametroGeneral PG
	INNER JOIN Parametro P
		ON P.ParametroID = PG.ParametroID
	WHERE P.Clave = @ParametroDescripcion

	INSERT INTO @SubProductos(ProductoID)
	SELECT * 
	FROM dbo.FuncionSplit(@Valor, '|')

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
		ON (P.SubFamiliaID = SB.SubFamiliaID)
	INNER JOIN Familia F
		ON (F.FamiliaID = SB.FamiliaID)
	INNER JOIN UnidadMedicion UM
		ON (P.UnidadID = UM.UnidadID
			AND @UnidadID IN (P.UnidadID, 0))			
	WHERE (P.Descripcion LIKE '%' + @Descripcion + '%' OR @Descripcion = '' )
			AND @ProductoID IN (P.ProductoID, 0)
			AND P.Activo = @Activo
			AND P.SubFamiliaID IN (@SubFamiliaID, 0)
			OR SB.FamiliaID IN (@FamiliaID)
			OR P.ProductoID IN (SELECT ProductoID FROM @SubProductos)

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
