USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerAnimalesPorXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerAnimalesPorXML]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerAnimalesPorXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Gilberto Carranza
-- Create date: 21/05/2015
-- Description: Sp para obtener los animales de un lote
-- Animal_ObtenerAnimalesPorXML '<ROOT><Animales><AnimalID>8084</AnimalID></Animales></ROOT>'
--=============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerAnimalesPorXML] 
@AnimalesXML	XML
AS
BEGIN

	SET NOCOUNT ON;

	CREATE TABLE #Animal (
		AnimalID BIGINT
		,Arete VARCHAR(15)
		,AreteMetalico VARCHAR(15)
		,FechaCompra SMALLDATETIME
		,TipoGanadoID INT
		,TipoGanado VARCHAR(50)
		,Sexo CHAR
		,CalidadGanadoID INT
		,ClasificacionGanadoID INT
		,ClasificacionGanado VARCHAR(50)
		,PesoCompra INT
		,OrganizacionIDEntrada INT
		,FolioEntrada BIGINT
		,PesoLlegada INT
		,Paletas INT
		,CausaRechadoID INT
		,Venta BIT
		,Cronico BIT
		,Activo BIT
		)
	CREATE TABLE #AnimalMovimiento (
		AnimalID BIGINT
		,AnimalMovimientoID BIGINT
		,OrganizacionID INT
		,CorralID INT
		,LoteID INT
		,FechaMovimiento SMALLDATETIME
		,Peso INT
		,Temperatura DECIMAL
		,TipoMovimientoID INT
		,TrampaID INT
		,OperadorID INT
		,Observaciones VARCHAR(255)
		,Activo			BIT
		)

	CREATE TABLE #tAnimales
	(
		AnimalID	INT
		, Activo	BIT
	)
	INSERT INTO #tAnimales
	SELECT t.item.value('./AnimalID[1]', 'BIGINT') AS AnimalID
		,  t.item.value('./Activo[1]', 'BIT') AS Activo
	FROM @AnimalesXML.nodes('ROOT/Animales') AS T (item)

	INSERT #AnimalMovimiento (
		AnimalID
		,AnimalMovimientoID
		,OrganizacionID
		,CorralID
		,LoteID
		,FechaMovimiento
		,Peso
		,Temperatura
		,TipoMovimientoID
		,TrampaID
		,OperadorID
		,Observaciones
		,Activo
		)
	SELECT AnimalID
		,AnimalMovimientoID
		,OrganizacionID
		,CorralID
		,LoteID
		,FechaMovimiento
		,Peso
		,Temperatura
		,TipoMovimientoID
		,TrampaID
		,OperadorID
		,Observaciones
		,Activo
	FROM 
	(
		SELECT AM.AnimalID
			,AM.AnimalMovimientoID
			,AM.OrganizacionID
			,AM.CorralID
			,AM.LoteID
			,AM.FechaMovimiento
			,AM.Peso
			,AM.Temperatura
			,AM.TipoMovimientoID
			,AM.TrampaID
			,AM.OperadorID
			,AM.Observaciones
			,x.Activo
		FROM AnimalMovimiento AM(NOLOCK)
		INNER JOIN #tAnimales x ON (AM.AnimalID = x.AnimalID)
		WHERE AM.Activo = 1
		UNION 
		SELECT AM.AnimalID
			,AM.AnimalMovimientoID
			,AM.OrganizacionID
			,AM.CorralID
			,AM.LoteID
			,AM.FechaMovimiento
			,AM.Peso
			,AM.Temperatura
			,AM.TipoMovimientoID
			,AM.TrampaID
			,AM.OperadorID
			,AM.Observaciones
			,x.Activo
		FROM AnimalMovimientoHistorico AM(NOLOCK)
		INNER JOIN #tAnimales x ON (AM.AnimalID = x.AnimalID)
		WHERE AM.Activo = 1
	) A

	INSERT INTO #Animal (
		AnimalID
		,Arete
		,AreteMetalico
		,FechaCompra
		,TipoGanadoID
		,TipoGanado
		,Sexo
		,CalidadGanadoID
		,ClasificacionGanadoID
		,ClasificacionGanado
		,PesoCompra
		,OrganizacionIDEntrada
		,FolioEntrada
		,PesoLlegada
		,Paletas
		,CausaRechadoID
		,Venta
		,Cronico
		)
	SELECT AnimalID
		,Arete
		,AreteMetalico
		,FechaCompra
		,TipoGanadoID
		,TipoGanado
		,Sexo
		,CalidadGanadoID
		,ClasificacionGanadoID
		,ClasificacionGanado
		,PesoCompra
		,OrganizacionIDEntrada
		,FolioEntrada
		,PesoLlegada
		,Paletas
		,CausaRechadoID
		,Venta
		,Cronico
	FROM
	(
		SELECT a.AnimalID
			,Arete
			,AreteMetalico
			,FechaCompra
			,tg.TipoGanadoID
			,tg.Descripcion [TipoGanado]
			,tg.Sexo
			,CalidadGanadoID
			,cg.ClasificacionGanadoID
			,cg.Descripcion [ClasificacionGanado]
			,PesoCompra
			,OrganizacionIDEntrada
			,a.FolioEntrada
			,PesoLlegada
			,Paletas
			,CausaRechadoID
			,Venta
			,Cronico
		FROM Animal a(NOLOCK)
		INNER JOIN #AnimalMovimiento am ON a.AnimalID = am.AnimalID
											AND a.Activo = AM.Activo
		INNER JOIN TipoGanado tg ON a.TipoGanadoID = tg.TipoGanadoID
		INNER JOIN ClasificacionGanado cg ON a.ClasificacionGanadoID = cg.ClasificacionGanadoID	
		UNION
		SELECT a.AnimalID
			,Arete
			,AreteMetalico
			,FechaCompra
			,tg.TipoGanadoID
			,tg.Descripcion [TipoGanado]
			,tg.Sexo
			,CalidadGanadoID
			,cg.ClasificacionGanadoID
			,cg.Descripcion [ClasificacionGanado]
			,PesoCompra
			,OrganizacionIDEntrada
			,a.FolioEntrada
			,PesoLlegada
			,Paletas
			,CausaRechadoID
			,Venta
			,Cronico
		FROM AnimalHistorico a(NOLOCK)
		INNER JOIN #AnimalMovimiento am ON a.AnimalID = am.AnimalID
											AND a.Activo = AM.Activo
		INNER JOIN TipoGanado tg ON a.TipoGanadoID = tg.TipoGanadoID
		INNER JOIN ClasificacionGanado cg ON a.ClasificacionGanadoID = cg.ClasificacionGanadoID
		WHERE a.Activo = 1
	) A

	SELECT AnimalID
		,Arete
		,AreteMetalico
		,FechaCompra
		,TipoGanadoID
		,TipoGanado
		,Sexo
		,CalidadGanadoID
		,ClasificacionGanadoID
		,ClasificacionGanado
		,PesoCompra
		,OrganizacionIDEntrada
		,FolioEntrada
		,PesoLlegada
		,Paletas
		,CausaRechadoID
		,Venta
		,Cronico
	FROM #Animal

	SELECT AnimalID
		,AnimalMovimientoID
		,OrganizacionID
		,CorralID
		,LoteID
		,FechaMovimiento
		,Peso
		,Temperatura
		,TipoMovimientoID
		,TrampaID
		,OperadorID
		,Observaciones
	FROM #AnimalMovimiento
	
	DROP TABLE #Animal
	DROP TABLE #AnimalMovimiento
	DROP TABLE #tAnimales

	SET NOCOUNT OFF;
END

GO
