USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorOrganizacionTipoAlmacenPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerPorOrganizacionTipoAlmacenPagina]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorOrganizacionTipoAlmacenPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 11/07/2014
-- Description: Obtiene los Almacenes por Organzaci�n y un conjunto de tipos de almacen.
-- SpName     : Almacen_ObtenerPorOrganizacionTipoAlmacenPagina 0, '' ,1, 1,'6|8', 1,15
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerPorOrganizacionTipoAlmacenPagina] 
	@AlmacenID INT
	,@Descripcion VARCHAR(50)
	,@OrganizacionID INT 	
	,@Activo BIT
	,@FiltroTipoAlmacen varchar(500)
	,@Inicio INT
	,@Limite INT
AS
Begin 
if @Descripcion is null
begin
	set @Descripcion = ''
end
declare @TipoAlmacen as TABLE
(
	TipoAlmacenID INT primary key
)
if(@FiltroTipoAlmacen = '')	
BEGIN
	INSERT INTO @TipoAlmacen(TipoAlmacenID)
	SELECT FamiliaID 
	FROM Familia	
END
else
BEGIN
INSERT INTO @TipoAlmacen(TipoAlmacenID)
	SELECT * 
	FROM dbo.FuncionSplit(@FiltroTipoAlmacen, '|')	
END
SET @Descripcion = '%' + replace(@Descripcion, '%','') + '%'
SELECT ROW_NUMBER() OVER (ORDER BY a.Descripcion ASC) AS RowNum
		,a.AlmacenID
		,a.OrganizacionID
		,o.Descripcion as [Organizacion]
		,a.CodigoAlmacen
		,a.Descripcion
		,a.TipoAlmacenID
		,ta.Descripcion as [TipoAlmacen]
		,a.CuentaInventario
		,a.CuentaInventarioTransito
		,a.CuentaDiferencias
		,a.Activo
		,a.FechaCreacion
		,a.UsuarioCreacionID
		,a.FechaModificacion
		,a.UsuarioModificacionID
		Into #Datos
FROM Almacen a
INNER JOIN Organizacion o ON o.OrganizacionID = a.OrganizacionID
INNER JOIN TipoAlmacen ta ON ta.TipoAlmacenID = a.TipoAlmacenID
WHERE @OrganizacionID in (a.OrganizacionID, 0)
	AND @AlmacenID IN (a.AlmacenID, 0)
	AND (a.Descripcion LIKE @Descripcion)
	AND a.Activo = @Activo
	AND EXISTS(select '' From @TipoAlmacen WHERE TipoAlmacenID = a.TipoAlmacenID)
	SELECT *
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(AlmacenID) AS [TotalReg]
	FROM #Datos
	DROP TABLE #Datos
END

GO
