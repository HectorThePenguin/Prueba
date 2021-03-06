USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteMedicamentosAplicadosSanidad_Obtener]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteMedicamentosAplicadosSanidad_Obtener]
GO
/****** Object:  StoredProcedure [dbo].[ReporteMedicamentosAplicadosSanidad_Obtener]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ReporteMedicamentosAplicadosSanidad_Obtener]
	-- =============================================
	-- Author: Gumaro Lugo
	-- Create date: 10/08/2014
	-- Description: Obtiene los datos para reporte de de medicamentos aplicados a sanidad
	-- ReporteMedicamentosAplicadosSanidad_Obtener 1,'20150205', '20150211'
	-- =============================================
	@OrganizacionID INT
    ,@AlmacenID INT
	,@FechaInicial DATE
	,@FechaFinal DATE
AS
BEGIN
	DECLARE @Parametro VARCHAR(250)
	DECLARE @TipoMovimientoEntradaEnfermeria INT
	,@TipoMovimientoEntradaSalidaEnfermeria INT
	,@TipoMovimientoSalidaEnfermeria INT
	SET @TipoMovimientoEntradaEnfermeria = 7
	SET @TipoMovimientoEntradaSalidaEnfermeria = 9
	SET @TipoMovimientoSalidaEnfermeria = 10
	DECLARE @FechaInicio DATE
	DECLARE @FechaFin	 DATE
	SET @FechaInicio = @FechaInicial
	SET @FechaFin = @FechaFinal
	CREATE TABLE #MEDICAMENTOS
	(
		TipoTratamientoID INT
		,TipoTratamiento VARCHAR(50)
		,ProductoID INT
		,Producto VARCHAR(100)
		,Cantidad decimal(14,2)
		,Precio decimal(10,2)
		,Unidad varchar(50)
	)
	SELECT @Parametro = Valor
	FROM ParametroGeneral PG
	INNER JOIN Parametro P ON (P.ParametroID = PG.ParametroID)
	WHERE Clave = 'ProductosEnfermeriaTotal'
	CREATE TABLE #tMovimientos
	(
		TipoMovimientoID		INT
		, AnimalMovimientoID	BIGINT
	)
	INSERT INTO #tMovimientos
	SELECT TipoMovimientoID
		,  AnimalMovimientoID
	FROM AnimalMovimiento am
	WHERE
		am.OrganizacionID = @OrganizacionID 
		AND CAST(am.FechaMovimiento as DATE) between @FechaInicio AND @FechaFin
		AND am.TipoMovimientoID in (@TipoMovimientoEntradaEnfermeria,@TipoMovimientoEntradaSalidaEnfermeria, @TipoMovimientoSalidaEnfermeria)
	CREATE TABLE #tMovimientosAlmacen
	(		
		AlmacenMovimientoID BIGINT
		, AnimalMovimientoID BIGINT
		, AlmacenID			INT
	)
	INSERT INTO #tMovimientosAlmacen
	SELECT Amov.AlmacenMovimientoID
		,  AM.AnimalMovimientoID
		,  Amov.AlmacenID
	FROM #tMovimientos am 
	INNER JOIN AlmacenMovimiento amov(NOLOCK) ON am.AnimalMovimientoID = amov.AnimalMovimientoID
  WHERE (amov.AlmacenID = @AlmacenID OR @AlmacenID = 0)
	CREATE TABLE #tMovimientosAlmacenDetalle
	(
		AlmacenMovimientoID	BIGINT
		, Cantidad			DECIMAL(18,2)
		, ProductoID		INT	
	)
	INSERT INTO #tMovimientosAlmacenDetalle
	SELECT AMD.AlmacenMovimientoID
		,  AMD.Cantidad
		,  AMD.ProductoID
	FROM #tMovimientosAlmacen MA
	INNER JOIN AlmacenMovimientoDetalle AMD
		ON (MA.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
	INSERT INTO #MEDICAMENTOS
	SELECT
	tm.TipoMovimientoID AS TipoTratamientoID
	,tm.Descripcion AS TipoTratamiento
	,pro.ProductoID 
	,pro.Descripcion AS Producto
	,amovd.Cantidad
	,ai.PrecioPromedio
	,um.Descripcion AS Unidad
	FROM #tMovimientos am 
	INNER JOIN TipoMovimiento tm ON am.TipoMovimientoID = tm.TipoMovimientoID
	INNER JOIN #tMovimientosAlmacen amov ON am.AnimalMovimientoID = amov.AnimalMovimientoID
	INNER JOIN #tMovimientosAlmacenDetalle amovd on amov.AlmacenMovimientoID = amovd.AlmacenMovimientoID
	INNER JOIN AlmacenInventario ai on (amov.AlmacenID = ai.AlmacenID AND amovd.ProductoID = ai.ProductoID)
	INNER JOIN Producto pro ON ai.ProductoID = pro.ProductoID
	INNER JOIN UnidadMedicion um on pro.UnidadID = um.UnidadID
	CREATE TABLE #tProductos
	(
		ProductoID INT
	)
	INSERT INTO #tProductos
	SELECT Registros FROM dbo.FuncionSplit(@Parametro,'|')
	SELECT
	M.TipoTratamientoID
		,M.TipoTratamiento 
		,M.ProductoID 
		,M.Producto 
		,M.Cantidad 
		,M.Precio 
		,M.Unidad 
		, CASE WHEN M.ProductoID IS NULL THEN 0
		  ELSE 1 END AS ContadorCabezas
	FROM #MEDICAMENTOS M
	LEFT OUTER JOIN #tProductos tP
		ON (M.ProductoID = tP.ProductoID)
	DROP TABLE #tProductos
	DROP TABLE #MEDICAMENTOS
	DROP TABLE #tMovimientos
	DROP TABLE #tMovimientosAlmacen
	DROP TABLE #tMovimientosAlmacenDetalle
END

GO
