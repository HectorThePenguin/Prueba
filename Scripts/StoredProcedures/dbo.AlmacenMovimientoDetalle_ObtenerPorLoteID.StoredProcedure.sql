USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerPorLoteID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerPorLoteID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerPorLoteID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 27/06/2014
-- Description: Obtiene el detalle de movimientos por almacenid y tipomovimiento
-- SpName     : EXEC AlmacenMovimientoDetalle_ObtenerPorLoteID 2,'<ROOT> <Datos> <tipoMovimiento>20</tipoMovimiento></Datos> </ROOT>'
--======================================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerPorLoteID]
@AlmacenInventarioLoteID INT,
@XmlTiposMovimiento XML
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #tipoMovimiento
	(
		TipoMovimientoID INT
	)
	INSERT INTO #tipoMovimiento
	SELECT DISTINCT T.N.value('./tipoMovimiento[1]','INT') AS TipoMovimientoID
	FROM @XmlTiposMovimiento.nodes('/ROOT/Datos') as T(N)
	SELECT 
		AMD.AlmacenMovimientoDetalleID,
		AMD.AlmacenMovimientoID,
		AMD.AlmacenInventarioLoteID,
		AMD.ContratoID,
		AMD.Piezas,
		AMD.TratamientoID,
		AMD.ProductoID,
		AMD.Precio,
		AMD.Cantidad,
		AMD.Importe,
		AMD.FechaCreacion,
		AMD.UsuarioCreacionID
	FROM AlmacenMovimientoDetalle (NOLOCK) AMD
	INNER JOIN AlmacenMovimiento (NOLOCK) AM ON(AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID)
	INNER JOIN #tipoMovimiento TM ON TM.TipoMovimientoID = AM.TipoMovimientoID
	WHERE AMD.AlmacenInventarioLoteID = @AlmacenInventarioLoteID
	SET NOCOUNT OFF;
END

GO
