USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerCompletoPorFamilia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerCompletoPorFamilia]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerCompletoPorFamilia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 11/07/2014
-- Description: Obtiene los Productos filtrados por la Familia
-- SpName     : Producto_ObtenerCompletoPorFamilia 0,'', 1,'2|3|4|5', 1,15
--======================================================
CREATE PROCEDURE [dbo].[Producto_ObtenerCompletoPorFamilia] @ProductoID INT
	,@Descripcion VARCHAR(50)
	,@Activo BIT
	,@FiltroFamilia varchar(500)
	,@Inicio INT
	,@Limite INT
AS
Begin 
if @Descripcion is null
begin
	set @Descripcion = ''
end
declare @FamiliasFiltro as TABLE
(
	FamiliaID INT primary key
)
if(@FiltroFamilia = '')	
BEGIN
	INSERT INTO @FamiliasFiltro(FamiliaID)
	SELECT FamiliaID 
	FROM Familia	
END
else
BEGIN
INSERT INTO @FamiliasFiltro(FamiliaID)
	SELECT * 
	FROM dbo.FuncionSplit(@FiltroFamilia, '|')	
END
SET @Descripcion = '%' + replace(@Descripcion, '%','') + '%'
SELECT ROW_NUMBER() OVER (ORDER BY P.Descripcion ASC) AS RowNum
	,p.ProductoID
	,p.Descripcion
	,p.SubFamiliaID
	,sub.Descripcion as [DescripcionSubFamilia]
	,p.UnidadID
	,um.Descripcion as [DescripcionUnidad]
	,p.ManejaLote
	,p.Activo
	,p.FechaCreacion
	,p.UsuarioCreacionID
	,p.FechaModificacion
	,p.UsuarioModificacionID
	,fa.FamiliaID
	,fa.Descripcion as [DescripcionFamilia]
		Into #Datos
FROM Producto p
INNER JOIN SubFamilia sub ON p.SubFamiliaID = sub.SubFamiliaID
INNER JOIN Familia fa ON sub.FamiliaId = fa.FamiliaID
INNER JOIN UnidadMedicion um on um.UnidadID = p.UnidadID
WHERE @ProductoID IN (p.ProductoID, 0)
	AND (p.Descripcion LIKE @Descripcion)
	AND p.Activo = @Activo
	AND EXISTS(select '' From @FamiliasFiltro where FamiliaId = fa.FamiliaID)
	SELECT *
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ProductoID) AS [TotalReg]
	FROM #Datos
	DROP TABLE #Datos
END

GO
