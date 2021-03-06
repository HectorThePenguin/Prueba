USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteMedicamentosAplicados_Obtener]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteMedicamentosAplicados_Obtener]
GO
/****** Object:  StoredProcedure [dbo].[ReporteMedicamentosAplicados_Obtener]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	-- =============================================
	-- Author: Jorge Luis Vel?zquez Araujo
	-- Create date: 24/04/2014
	-- Description: Obtiene los datos para reporte de Cr?nicos en Recuperaci?n
	-- propuesta
	-- ReporteMedicamentosAplicados_Obtener 1,'20150205', '20150211'
	-- =============================================
CREATE PROCEDURE [dbo].[ReporteMedicamentosAplicados_Obtener]
	@OrganizacionID INT
	,@FechaInicial DATE
	,@FechaFinal DATE
AS
BEGIN
	declare @TipoMovimientoCorte INT
	,@TipoMovimientoReimplante INT
	,@TipoMovimientoCorteTransferencia INT
	,@CantidadCabezasCorte int
	,@CantidadCabezasReimplante int
	,@CantidadCabezasCorteTransferencia int
	set @TipoMovimientoCorte = 5
	set @TipoMovimientoReimplante = 6
	set @TipoMovimientoCorteTransferencia = 13
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
		,Cantidad decimal(18,2)
		,Precio decimal(18,4)
		,Unidad varchar(50)
		,AnimalID INT
	)
	INSERT INTO #MEDICAMENTOS
	SELECT
	tm.TipoMovimientoID AS TipoTratamientoID
	,tm.Descripcion AS TipoTratamiento
	,pro.ProductoID 
	,pro.Descripcion AS Producto
	,amovd.Cantidad
	,ai.PrecioPromedio
	,um.Descripcion AS Unidad
	,am.AnimalID AS AnimalID
	FROM AnimalMovimiento am 
	INNER JOIN TipoMovimiento tm ON am.TipoMovimientoID = tm.TipoMovimientoID
	INNER JOIN AlmacenMovimiento amov ON am.AnimalMovimientoID = amov.AnimalMovimientoID
	INNER JOIN AlmacenMovimientoDetalle amovd on amov.AlmacenMovimientoID = amovd.AlmacenMovimientoID
	INNER JOIN AlmacenInventario ai on (amov.AlmacenID = ai.AlmacenID AND amovd.ProductoID = ai.ProductoID)
	INNER JOIN Producto pro ON ai.ProductoID = pro.ProductoID
	INNER JOIN UnidadMedicion um on pro.UnidadID = um.UnidadID
	WHERE
		am.OrganizacionID = @OrganizacionID 
		AND CAST(am.FechaMovimiento AS DATE) BETWEEN @FechaInicio AND @FechaFin
		AND am.TipoMovimientoID in (@TipoMovimientoCorte,@TipoMovimientoReimplante, @TipoMovimientoCorteTransferencia)
		-- Calcula el total de cabezas para "corte". Por requerimeinto se calcula la cantidad de cabezas que fueron medicados con 514 (Caliber 7 50 DS)
	-- y este es el valor asigando como el total de cabezas de ganado
	set @CantidadCabezasCorte =  (select count( AnimalID) from #MEDICAMENTOS where TipoTratamientoID = @TipoMovimientoCorte and ProductoID = 514)
	-- Calcula el total de cabezas para el tipo "corte por transferencia". Por requerimeinto se calcula la cantidad de cabezas que fueron medicados con 514 (Caliber 7 50 DS)
	-- y este es el valor asigando como el total de cabezas de ganado para este tipo de  "corte por tranferencia"
	set @CantidadCabezasCorteTransferencia =  (select count( AnimalID) from #MEDICAMENTOS where TipoTratamientoID = @TipoMovimientoCorteTransferencia and ProductoID = 514)
	--Aqui si se calcula el total de cabezas que fueron medicados dentro de la categoria "Reimplante"
	-- Por requerimiento, se suman los componentes  97 (200 con tylan) y 98 ( TE-S con TYLAN)
	set @CantidadCabezasReimplante =  ((select count( AnimalID) from #MEDICAMENTOS where TipoTratamientoID = @TipoMovimientoReimplante and ProductoID = 97) +
									  (select count( AnimalID) from #MEDICAMENTOS where TipoTratamientoID = @TipoMovimientoReimplante and ProductoID = 98))
	SELECT
	TipoTratamientoID
		,TipoTratamiento 
		,ProductoID 
		,Producto 
		,Cantidad 
		,Precio 
		,Unidad 
		,AnimalID = 
			case TipoTratamientoID
			  when 5 then @CantidadCabezasCorte
			  when 6 then @CantidadCabezasReimplante
			  when 13 then @CantidadCabezasCorteTransferencia
			end
	FROM #MEDICAMENTOS
END

GO
