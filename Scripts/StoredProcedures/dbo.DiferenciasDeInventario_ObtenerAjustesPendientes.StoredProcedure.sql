USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DiferenciasDeInventario_ObtenerAjustesPendientes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DiferenciasDeInventario_ObtenerAjustesPendientes]
GO
/****** Object:  StoredProcedure [dbo].[DiferenciasDeInventario_ObtenerAjustesPendientes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 29/06/2014
-- Description: Procedimiento que obtiene los ajustes pendientes
-- SpName     : EXEC DiferenciasDeInventario_ObtenerAjustesPendientes '<ROOT><Datos><EstatusID>35</EstatusID></Datos><Datos><EstatusID>36</EstatusID></Datos><Datos><EstatusID>37</EstatusID></Datos></ROOT>', '<ROOT><Datos><TipoMovimientoID>31</TipoMovimientoID></Datos><Datos><TipoMovimientoID>32</TipoMovimientoID></Datos></ROOT>', 5
--======================================================
CREATE PROCEDURE [dbo].[DiferenciasDeInventario_ObtenerAjustesPendientes]
@XmlEstatus XML,
@XmlTipoMovimiento XML,
@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #estatus
	(
		EstatusID INT
	)
	INSERT INTO #estatus
	SELECT DISTINCT T.N.value('./EstatusID[1]','INT') AS EstatusID
	FROM @XmlEstatus.nodes('/ROOT/Datos') as T(N)
	CREATE TABLE #tipoMovimiento
	(
		TipoMovimientoID INT
	)
	INSERT INTO #tipoMovimiento
	SELECT DISTINCT T.N.value('./TipoMovimientoID[1]','INT') AS TipoMovimientoID
	FROM @XmlTipoMovimiento.nodes('/ROOT/Datos') as T(N)
		SELECT 
				AM.FolioMovimiento,
			  AM.AlmacenMovimientoID,
			  AM.TipoMovimientoID,
			  AM.AlmacenID,
			  AM.Observaciones,
			  A.Descripcion AS DescripcionAlmacen,
			  AMD.ProductoID,
			  P.Descripcion AS ProductoDescripcion,
			  AMD.AlmacenInventarioLoteID,
			  AIL.Lote,
			  AM.Status,
			  E.Descripcion AS DescripcionEstatus,
			  AMD.AlmacenMovimientoDetalleID,
			  AMD.Cantidad,
			  AMD.Precio
		FROM AlmacenMovimiento AM (NOLOCK)
		INNER JOIN AlmacenMovimientoDetalle AMD (NOLOCK)  ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID
		INNER JOIN AlmacenInventarioLote AIL (NOLOCK) ON AIL.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID
		INNER JOIN Almacen A (NOLOCK) ON A.AlmacenID = AM.AlmacenID
		INNER JOIN Producto P (NOLOCK)  ON P.ProductoID = AMD.ProductoID
		INNER JOIN Estatus E (NOLOCK) ON E.EstatusID = AM.Status
		INNER JOIN #estatus TE ON TE.EstatusID = AM.Status
		INNER JOIN #tipoMovimiento TTM ON TTM.TipoMovimientoID = AM.TipoMovimientoID
		WHERE AM.UsuarioCreacionID = @UsuarioCreacionID
END

GO
