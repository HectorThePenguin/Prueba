USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorPaginaPoliza]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerPorPaginaPoliza]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorPaginaPoliza]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 15/07/2014
-- Description: 
-- SpName     : Almacen_ObtenerPorPaginaPoliza 0,'',0,0,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerPorPaginaPoliza]
@AlmacenID int,
@Descripcion varchar(50),
@OrganizacionID int,
@TipoAlmacenID int,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY A.Descripcion ASC) AS [RowNum],
		A.AlmacenID,
		A.OrganizacionID,
		A.CodigoAlmacen,
		A.Descripcion,
		A.TipoAlmacenID,
		A.CuentaInventario,
		A.CuentaInventarioTransito,
		A.CuentaDiferencias,
		A.Activo
	INTO #Almacen
	FROM Almacen A
	INNER JOIN AlmacenMovimiento AM
		ON (A.AlmacenID = AM.AlmacenID
			AND AM.PolizaGenerada = 1)
	WHERE (A.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	And @OrganizacionID in (A.OrganizacionID, 0)
	And @TipoAlmacenID in (A.TipoAlmacenID, 0)
	AND A.Activo = @Activo
	SELECT
		a.AlmacenID,
		a.OrganizacionID,
		o.Descripcion as [Organizacion],
		a.CodigoAlmacen,
		a.Descripcion,
		a.TipoAlmacenID,
		ta.Descripcion as [TipoAlmacen],
		a.CuentaInventario,
		a.CuentaInventarioTransito,
		a.CuentaDiferencias,
		a.Activo
	FROM #Almacen a
	inner join Organizacion o on o.OrganizacionID = a.OrganizacionID
	inner join TipoAlmacen ta on ta.TipoAlmacenID = a.TipoAlmacenID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(AlmacenID) AS [TotalReg]
	FROM #Almacen
	DROP TABLE #Almacen
	SET NOCOUNT OFF;
END

GO
