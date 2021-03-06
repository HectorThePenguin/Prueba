USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMoviento_ObtenerSubProductos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMoviento_ObtenerSubProductos]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMoviento_ObtenerSubProductos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 10/12/2014
-- Description: Procedimiento para obtener los subproductos de la premezcla
-- SpName     : AlmacenMoviento_ObtenerSubProductos '<ROOT><AlmacenMovimiento><ProductoID>317</ProductoID><FechaMovimiento>20141122</FechaMovimiento></AlmacenMovimiento></ROOT>'
--======================================================
CREATE PROCEDURE [dbo].[AlmacenMoviento_ObtenerSubProductos]
@XmlDatosProductosPremezcla XML
AS
BEGIN
	SET NOCOUNT ON
		CREATE TABLE #tProductoPremezcla
		(
			ProductoID INT
			, FechaMovimiento DATETIME
		)
		CREATE TABLE #tSubProductos
		(
			ProductoID INT
		)
		INSERT INTO #tProductoPremezcla
		SELECT DISTINCT AMD.ProductoID
			,  AM.FechaMovimiento
		FROM AlmacenMovimiento AM
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
		INNER JOIN 
		(
			SELECT T.N.value('./ProductoID[1]','INT') AS ProductoID
				,  T.N.value('./FechaMovimiento[1]','DATETIME') AS FechaMovimiento
			FROM @XmlDatosProductosPremezcla.nodes('/ROOT/AlmacenMovimiento') as T(N)
		) A ON (CAST(AM.FechaMovimiento AS DATE) = CAST(A.FechaMovimiento AS DATE)
				AND AMD.ProductoID = A.ProductoID)
		GROUP BY AMD.ProductoID
			  ,  AM.FechaMovimiento
		INSERT INTO #tSubProductos
		SELECT PD.ProductoID
		FROM #tProductoPremezcla PP
		INNER JOIN Premezcla P
			ON (PP.ProductoID = P.ProductoID)
		INNER JOIN PremezclaDetalle PD
			ON (P.PremezclaID = PD.PremezclaID)
		GROUP BY PD.ProductoID
		SELECT AM.AlmacenID
			,  AM.AlmacenMovimientoID
			,  AM.FechaMovimiento
			,  AMD.Cantidad
			,  AMD.Precio
			,  AMD.Importe
			,  SP.ProductoID
		INTO #tMovimientosSubProductos
		FROM AlmacenMovimiento AM
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID
				AND AM.FechaMovimiento = AMD.FechaCreacion)
		INNER JOIN #tSubProductos SP
			ON (AMD.ProductoID = SP.ProductoID)
		INNER JOIN #tProductoPremezcla PP
			ON (AM.FechaMovimiento = PP.FechaMovimiento)
			AND AM.Status = 21
		GROUP BY AM.AlmacenID
			,  AM.AlmacenMovimientoID
			,  AM.FechaMovimiento
			,  AMD.Cantidad
			,  AMD.Precio
			,  AMD.Importe
			,  SP.ProductoID
		DECLARE @Premezcla TINYINT
		SET @Premezcla = 6
		SELECT AM.AlmacenID
			,  AM.AlmacenMovimientoID
			,  AM.FechaMovimiento
			,  AMD.ProductoID
			,  AMD.Precio
			,  AMD.Cantidad
			,  AMD.Importe
		FROM #tMovimientosSubProductos MSP
		INNER JOIN  AlmacenMovimiento AM
			ON (MSP.FechaMovimiento = AM.FechaMovimiento)
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID
				AND MSP.ProductoID <> AMD.ProductoID)
		INNER JOIN Producto P
			ON (AMD.ProductoID = P.ProductoID)
		INNER JOIN SubFamilia SF
			ON (P.SubFamiliaID = SF.SubFamiliaID
				AND SF.FamiliaID = @Premezcla)
		GROUP BY AM.AlmacenID
			,  AM.AlmacenMovimientoID
			,  AM.FechaMovimiento
			,  AMD.ProductoID
			,  AMD.Precio
			,  AMD.Cantidad
			,  AMD.Importe
		DROP TABLE #tProductoPremezcla
		DROP TABLE #tSubProductos
		DROP TABLE #tMovimientosSubProductos
	SET NOCOUNT OFF
END

GO
