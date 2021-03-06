USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_ObtenerPorPaginaTratamiendoID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoProducto_ObtenerPorPaginaTratamiendoID]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_ObtenerPorPaginaTratamiendoID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 20/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TratamientoProducto_ObtenerPorPaginaTratamiendoID
--======================================================
CREATE PROCEDURE [dbo].[TratamientoProducto_ObtenerPorPaginaTratamiendoID] @TratamientoProductoID INT
	,@TratamientoID INT
	,@Activo BIT
	,@Inicio INT
	,@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY tp.TratamientoProductoID ASC
			) AS [RowNum]
		,tp.TratamientoProductoID
		,tr.TratamientoID
		,tr.CodigoTratamiento
		,pr.ProductoID
		,pr.Descripcion [Producto]
		,sf.SubFamiliaID
		,sf.Descripcion [SubFamilia]
		,fa.FamiliaID
		,fa.Descripcion [Familia]
		,tp.Dosis
		,tp.Activo
	INTO #Datos
	FROM TratamientoProducto tp
	INNER JOIN Tratamiento tr ON tp.TratamientoID = tr.TratamientoID
	INNER JOIN Producto pr on tp.ProductoID = pr.ProductoID
	INNER JOIN SubFamilia sf on pr.SubFamiliaID = sf.SubFamiliaID
	INNER JOIN Familia fa on sf.FamiliaID = fa.FamiliaID
	WHERE tp.Activo = @Activo
		AND tr.TratamientoID = @TratamientoID
	SELECT TratamientoProductoID
		,TratamientoID
		,CodigoTratamiento
		,ProductoID
		,Producto
		,SubFamiliaID
		,SubFamilia
		,FamiliaID
		,Familia
		,Dosis
		,Activo
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio
			AND @Limite
	SELECT COUNT(TratamientoProductoID) AS [TotalReg]
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO
