USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Almacen_ObtenerPorPagina 0,'',0,0,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerPorPagina]
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
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		AlmacenID,
		OrganizacionID,
		CodigoAlmacen,
		Descripcion,
		TipoAlmacenID,
		CuentaInventario,
		CuentaInventarioTransito,
		CuentaDiferencias,
		Activo
	INTO #Almacen
	FROM Almacen
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	And @OrganizacionID in (OrganizacionID, 0)
	And @TipoAlmacenID in (TipoAlmacenID, 0)
	AND Activo = @Activo
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
		pr.ProveedorID,
		pr.Descripcion AS Proveedor,
		pr.CodigoSAP,
		a.Activo
	FROM #Almacen a
	inner join Organizacion o on o.OrganizacionID = a.OrganizacionID
	inner join TipoAlmacen ta on ta.TipoAlmacenID = a.TipoAlmacenID
	left join ProveedorAlmacen pa on a.AlmacenID = pa.AlmacenID
	left join Proveedor pr on pa.ProveedorID = pr.ProveedorID 
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(AlmacenID) AS [TotalReg]
	FROM #Almacen
	DROP TABLE #Almacen
	SET NOCOUNT OFF;
END

GO
