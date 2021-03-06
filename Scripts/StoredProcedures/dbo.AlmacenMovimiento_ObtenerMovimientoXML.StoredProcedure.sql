USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ObtenerMovimientoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimiento_ObtenerMovimientoXML]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ObtenerMovimientoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 07/07/2014
-- Description:  Obtiene el ultimo movimiento de los animales
-- AlmacenMovimiento_ObtenerMovimientoXML '<ROOT><AlmacenMovimiento><AlmacenMovimientoDetalleID>6473</AlmacenMovimientoDetalleID></AlmacenMovimiento></ROOT>'
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenMovimiento_ObtenerMovimientoXML]
@XmlAlmacenMovimiento XML
AS
BEGIN
	SET NOCOUNT ON
		CREATE TABLE #tMovimientos
		(
			AlmacenMovimientoDetalleID BIGINT
		)
		CREATE TABLE #MovimientosDetalle
		(
			AlmacenMovimientoDetalleID	BIGINT
			, AlmacenMovimientoID		BIGINT
			, TratamientoID				INT
			, ProductoID				INT
			, Precio					DECIMAL(18,2)
			, Cantidad					DECIMAL(18,2)
			, Importe					DECIMAL(18,2)
			, AlmacenInventarioLoteID	INT
			, ContratoID				INT
			, Piezas					INT
		)
		INSERT INTO #tMovimientos
		SELECT AnimalID = T.item.value('./AlmacenMovimientoDetalleID[1]', 'BIGINT')
		FROM  @XmlAlmacenMovimiento.nodes('ROOT/AlmacenMovimiento') AS T(item)
		INSERT INTO #MovimientosDetalle
		SELECT AMD.AlmacenMovimientoDetalleID
				, AMD.AlmacenMovimientoID
				, AMD.TratamientoID
				, AMD.ProductoID
				, AMD.Precio
				, AMD.Cantidad
				, AMD.Importe
				, AMD.AlmacenInventarioLoteID
				, AMD.ContratoID
				, AMD.Piezas
		FROM #tMovimientos M
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (M.AlmacenMovimientoDetalleID = AMD.AlmacenMovimientoDetalleID)
		SELECT AM.AlmacenMovimientoID
				,AM.AlmacenID
				,AM.TipoMovimientoID
				,AM.FolioMovimiento
				,AM.FechaMovimiento
				,AM.Observaciones
				,AM.Status
				,AM.AnimalMovimientoID
				,AM.ProveedorID
				,AM.PolizaGenerada
		FROM AlmacenMovimiento AM
		INNER JOIN #MovimientosDetalle MD
			ON (AM.AlmacenMovimientoID = MD.AlmacenMovimientoID)
		SELECT AMD.AlmacenMovimientoDetalleID
			, AMD.AlmacenMovimientoID
			, AMD.TratamientoID
			, AMD.ProductoID
			, AMD.Precio
			, AMD.Cantidad
			, AMD.Importe
			, AMD.AlmacenInventarioLoteID
			, AMD.ContratoID
			, AMD.Piezas
		FROM #MovimientosDetalle MD
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (MD.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
		DROP TABLE #tMovimientos
		DROP TABLE #MovimientosDetalle
	SET NOCOUNT OFF
END

GO
