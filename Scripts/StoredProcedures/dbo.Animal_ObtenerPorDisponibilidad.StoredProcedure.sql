USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerPorDisponibilidad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerPorDisponibilidad]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerPorDisponibilidad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 11/02/2014
-- Description:  Obtiene animales por su disponibilidad
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerPorDisponibilidad]
@XmlLote XML
, @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tLote
	(
		LoteID INT
	)

	INSERT INTO #tLote
	SELECT 
		t.item.value('./LoteID[1]', 'INT') AS LoteID
	FROM @XmlLote.nodes('ROOT/Lotes') AS T(item)

	DECLARE @Animal AS TABLE (
		AnimalID BIGINT
		,Arete VARCHAR(15)
		,AreteMetalico VARCHAR(15)
		,FechaCompra SMALLDATETIME
		,FechaEntrada SMALLDATETIME
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
		,LoteID INT
		)
	DECLARE @AnimalMovimiento AS TABLE (
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

	INSERT @AnimalMovimiento (
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
	INNER JOIN #tLote L
		ON (AM.LoteID = L.LoteID)
	WHERE OrganizacionID = @OrganizacionID
		AND Activo = 1

	INSERT INTO @Animal (
		AnimalID
		,Arete
		,AreteMetalico
		,FechaCompra
		,FechaEntrada
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
		,LoteID
		)
	SELECT a.AnimalID
		,Arete
		,AreteMetalico
		,FechaCompra
		,eg.FechaEntrada
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
		,am.LoteID
	FROM Animal a (nolock)
	INNER JOIN @AnimalMovimiento am ON a.AnimalID = am.AnimalID 
	INNER JOIN TipoGanado tg ON a.TipoGanadoID = tg.TipoGanadoID
	INNER JOIN ClasificacionGanado cg ON a.ClasificacionGanadoID = cg.ClasificacionGanadoID
	INNER JOIN EntradaGanado eg on eg.FolioEntrada = a.FolioEntrada AND eg.OrganizacionID = @OrganizacionID	
	WHERE a.Activo = 1

	SELECT AnimalID
		,Arete
		,AreteMetalico
		,FechaCompra
		,FechaEntrada
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
		,LoteID
	FROM @Animal

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
	FROM @AnimalMovimiento

	SET NOCOUNT OFF;
END

GO
