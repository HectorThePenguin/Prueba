USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerAnimalesPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_ObtenerAnimalesPorLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerAnimalesPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Gilberto Carranza
-- Create date: 28/02/2015
-- Description: Sp para obtener los animales de un lote
-- AnimalMovimiento_ObtenerAnimalesPorLoteXML '<ROOT><Lotes><LoteID>8084</LoteID></Lotes><Lotes><LoteID>8063</LoteID></Lotes></ROOT>'
--=============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_ObtenerAnimalesPorLoteXML] 
@LoteXML	XML
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
		)

	CREATE TABLE #tLote
	(
		LoteID	INT
	)
	INSERT INTO #tLote
	SELECT t.item.value('./LoteID[1]', 'INT') AS LoteID
	FROM @LoteXML.nodes('ROOT/Lotes') AS T (item)

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
		)
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
	FROM AnimalMovimiento AM(NOLOCK)
	INNER JOIN #tLote x ON (AM.LoteID = x.LoteID)
	WHERE AM.Activo = 1

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
	INNER JOIN TipoGanado tg ON a.TipoGanadoID = tg.TipoGanadoID
	INNER JOIN ClasificacionGanado cg ON a.ClasificacionGanadoID = cg.ClasificacionGanadoID
	WHERE a.Activo = 1

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
	DROP TABLE #tLote

	SET NOCOUNT OFF;
END

GO
