USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteCronicosRecuperacion_Obtener]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteCronicosRecuperacion_Obtener]
GO
/****** Object:  StoredProcedure [dbo].[ReporteCronicosRecuperacion_Obtener]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ReporteCronicosRecuperacion_Obtener]
	-- =============================================
	-- Author: Jorge Luis Vel�zquez Araujo
	-- Create date: 24/04/2014
	-- Description: Obtiene los datos para reporte de Cr�nicos en Recuperaci�n
	-- ReporteCronicosRecuperacion_Obtener 4,'20140101', '20141230'
	-- =============================================
	@OrganizacionID INT
	,@FechaInicial DATE
	,@FechaFinal DATE
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @EntradaEnfermeria INT
		,@TipoCorralCronicoEnRecuperacion INT
		,@TipoProblemaPrincipal INT
		,@TipoCorralProduccion INT
	SET @EntradaEnfermeria = 7
	SET @TipoCorralCronicoEnRecuperacion = 8
	SET @TipoProblemaPrincipal = 1
	SET @TipoCorralProduccion = 2
	CREATE TABLE #tMovimientosCorralEnfermeria (
		AnimalMovimientoID BIGINT
		,CorralID INT
		,AnimalID INT
		,Fecha SMALLDATETIME
		)
	CREATE TABLE #tMovimientosCorralProduccion (
		AnimalMovimientoID BIGINT
		,CorralID INT
		,Codigo VARCHAR(10)
		,AnimalID INT
		,Fecha SMALLDATETIME
		)
	INSERT INTO #tMovimientosCorralEnfermeria
	SELECT MAX(AM.AnimalMovimientoID)
		,AM.CorralID
		,AM.AnimalID
		,MAX(AM.FechaMovimiento) AS FechaMovimiento
	FROM AnimalMovimiento AM
	INNER JOIN Corral C ON (
			AM.CorralID = C.CorralID
			AND C.TipoCorralID = @TipoCorralCronicoEnRecuperacion
			)
	WHERE AM.OrganizacionID = @OrganizacionID
		AND AM.TipoMovimientoID = @EntradaEnfermeria
		AND CAST(AM.FechaMovimiento as DATE) >= @FechaInicial
		AND CAST(AM.FechaMovimiento as DATE) <= @FechaFinal
	GROUP BY AM.CorralID
		,AM.AnimalID
	INSERT INTO #tMovimientosCorralProduccion
	SELECT MAX(AM.AnimalMovimientoID)
		,AM.CorralID
		,C.Codigo
		,AM.AnimalID
		,MAX(AM.FechaMovimiento) AS FechaMovimiento
	FROM AnimalMovimiento AM
	INNER JOIN Corral C ON (
			AM.CorralID = C.CorralID
			AND C.TipoCorralID = @TipoCorralProduccion
			)
	INNER JOIN #tMovimientosCorralEnfermeria MCE ON (
			AM.AnimalID = MCE.AnimalID
			AND AM.FechaMovimiento <= MCE.Fecha
			)
	GROUP BY AM.CorralID
		,C.Codigo
		,AM.AnimalID
	SELECT DISTINCT MC.Fecha
		,E.Descripcion AS Enfermeria
		,A.Arete
		,TG.Sexo
		--,P.Descripcion AS Causa
		, LEFT((SELECT  STUFF((SELECT distinct ', '+ P.Descripcion  from #tMovimientosCorralEnfermeria tx
						INNER JOIN DeteccionAnimal dax on dax.AnimalMovimientoID = tx.AnimalMovimientoID
						INNER JOIN DeteccionSintomaAnimal DSA ON (DA.DeteccionAnimalID = DSA.DeteccionAnimalID)
						INNER JOIN ProblemaSintoma PS ON (DSA.SintomaID = PS.SintomaID)
						INNER JOIN Problema P ON (PS.ProblemaID = P.ProblemaID)
						INNER JOIN TipoProblema TP ON (
								P.TipoProblemaID = TP.TipoProblemaID
								AND TP.TipoProblemaID = @TipoProblemaPrincipal
								)
						WHERE TX.AnimalMovimientoID = MC.AnimalMovimientoID
				FOR XML PATH('')), 1, 1, '')), 255) as Causa
		,O.Nombre + ' ' + O.ApellidoPaterno + ' ' + O.ApellidoMaterno AS Detector
		,C.Codigo AS CorralEnfermeria
		,MC.AnimalID
		,MC.AnimalMovimientoID
		,dbo.ObtenerFechaSalidaEnfermeria(@OrganizacionID, MC.AnimalID, MC.Fecha) AS FechaAlta
	FROM #tMovimientosCorralEnfermeria MC
	INNER JOIN Corral C ON (MC.CorralID = C.CorralID)
	INNER JOIN EnfermeriaCorral EC ON (C.CorralID = EC.CorralID)
	INNER JOIN Animal A ON (MC.AnimalID = A.AnimalID)
	INNER JOIN TipoGanado TG ON (A.TipoGanadoID = TG.TipoGanadoID)
	INNER JOIN DeteccionAnimal DA ON (MC.AnimalMovimientoID = DA.AnimalMovimientoID)
	INNER JOIN Operador O ON (DA.OperadorID = O.OperadorID)
	INNER JOIN Enfermeria E ON (EC.EnfermeriaID = E.EnfermeriaID)
	INNER JOIN DeteccionSintomaAnimal DSA ON (DA.DeteccionAnimalID = DSA.DeteccionAnimalID)
	INNER JOIN ProblemaSintoma PS ON (DSA.SintomaID = PS.SintomaID)
	INNER JOIN Problema P ON (PS.ProblemaID = P.ProblemaID)
	INNER JOIN TipoProblema TP ON (
			P.TipoProblemaID = TP.TipoProblemaID
			AND TP.TipoProblemaID = @TipoProblemaPrincipal
			)
	SELECT MCP.AnimalID
		,MCP.AnimalMovimientoID
		,MCP.Codigo AS CorralProduccion
		,MCP.CorralID
		,MCP.Fecha
	FROM #tMovimientosCorralProduccion MCP
	SELECT T.CodigoTratamiento
		,p.Descripcion Producto
		,MC.AnimalID
		,MC.AnimalMovimientoID
		,ALM.FechaMovimiento
	FROM AnimalMovimiento MC
	INNER JOIN AlmacenMovimiento ALM ON (
			MC.AnimalMovimientoID = ALM.AnimalMovimientoID
			AND MC.TipoMovimientoID = @EntradaEnfermeria
			)
	INNER JOIN AlmacenMovimientoDetalle AMD ON (ALM.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
	INNER JOIN Tratamiento T ON (AMD.TratamientoID = T.TratamientoID)
	INNER JOIN Producto p on AMD.ProductoID = p.ProductoID
	SELECT A.FolioEntrada
		,O.OrganizacionID
		,O.Descripcion AS Organizacion
		,DATEDIFF(dd, EG.FechaEntrada, GETDATE()) AS DiasEngorda
		,EG.FechaEntrada AS FechaLlegada
		,A.AnimalID
	FROM #tMovimientosCorralEnfermeria MC
	INNER JOIN Animal A ON (MC.AnimalID = A.AnimalID)
	INNER JOIN EntradaGanado EG ON (A.FolioEntrada = EG.FolioEntrada)
	INNER JOIN Organizacion O ON (EG.OrganizacionOrigenID = O.OrganizacionID)
	DROP TABLE #tMovimientosCorralEnfermeria
	DROP TABLE #tMovimientosCorralProduccion
	SET NOCOUNT OFF
END

GO
