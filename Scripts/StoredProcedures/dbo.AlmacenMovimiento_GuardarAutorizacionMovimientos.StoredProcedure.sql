USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_GuardarAutorizacionMovimientos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimiento_GuardarAutorizacionMovimientos]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_GuardarAutorizacionMovimientos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================
-- Author: Emir Lezama
-- Create date: 12/12/2014
-- Description:  Actualiza el estatus de las solicitudes Autorizada o Rechazada en la tabla AlmacenMovimiento
-- Origen: APInterfaces
-- ================================================
CREATE PROCEDURE [dbo].[AlmacenMovimiento_GuardarAutorizacionMovimientos]
 @XmlSolicitudes XML,
 @DifInvAutorizadoID INT
AS
BEGIN
	SET NOCOUNT ON;
	/* Se crea tabla temporal para almacenar el XML */
	 DECLARE @AutorizacionMovimientosTemp AS TABLE
	(
		AlmacenMovimientoID INT,
		EstatusInventarioID INT
	)
	/* Se llena tabla temporal con info del XML */
	INSERT @AutorizacionMovimientosTemp(
			AlmacenMovimientoID,
			EstatusInventarioID
		)
	SELECT 
		AlmacenMovimientoID = T.item.value('./AlmacenMovimientoID[1]', 'INT'),
		EstatusID  = T.item.value('./EstatusInventarioID[1]', 'INT')
	FROM  @XmlSolicitudes.nodes('ROOT/AutorizacionMovimientos') AS T(item)
	UPDATE AM 
	SET AM.Status = Temp.EstatusInventarioID
	FROM AlmacenMovimiento AM INNER JOIN @AutorizacionMovimientosTemp AS Temp
	ON AM.AlmacenMovimientoID = Temp.AlmacenMovimientoID
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
	AIL.Cantidad AS CantidadLote,
	AIL.Importe,
	AIL.PrecioPromedio,
	AIL.AlmacenInventarioID,
	AM.Status,
	E.Descripcion AS DescripcionEstatus,
	AMD.AlmacenMovimientoDetalleID,
	AMD.Cantidad,
	AMD.Precio
	FROM AlmacenMovimiento AM (NOLOCK)
	INNER JOIN @AutorizacionMovimientosTemp AS Temp ON AM.AlmacenMovimientoID = Temp.AlmacenMovimientoID
	INNER JOIN AlmacenMovimientoDetalle AMD (NOLOCK)  ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID
	INNER JOIN AlmacenInventarioLote AIL (NOLOCK) ON AIL.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID
	INNER JOIN Almacen A (NOLOCK) ON A.AlmacenID = AM.AlmacenID
	INNER JOIN Producto P (NOLOCK)  ON P.ProductoID = AMD.ProductoID
	INNER JOIN Estatus E (NOLOCK) ON E.EstatusID = AM.Status
	WHERE Temp.EstatusInventarioID = @DifInvAutorizadoID
	SET NOCOUNT OFF;
 END

GO
