USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerAnimalesPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_ObtenerAnimalesPorLote]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerAnimalesPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/01/2014
-- Description: Sp para obtener los animales de un lote
-- AnimalMovimiento_ObtenerAnimalesPorLote 5,14934
--=============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_ObtenerAnimalesPorLote] @OrganizacionID INT
	,@LoteID INT
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
		,FechaEntrada SMALLDATETIME
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
	FROM AnimalMovimiento(NOLOCK)
	WHERE OrganizacionID = @OrganizacionID
		AND LoteID = @LoteID
		AND Activo = 1

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
		,FechaEntrada
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
		,eg.FechaEntrada
	FROM Animal a(NOLOCK)
	INNER JOIN #AnimalMovimiento am ON a.AnimalID = am.AnimalID
	INNER JOIN TipoGanado tg ON a.TipoGanadoID = tg.TipoGanadoID
	INNER JOIN ClasificacionGanado cg ON a.ClasificacionGanadoID = cg.ClasificacionGanadoID
	INNER JOIN EntradaGanado eg on a.FolioEntrada = eg.FolioEntrada and a.OrganizacionIDEntrada = @OrganizacionID and a.OrganizacionIDEntrada = eg.OrganizacionID
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
		,FechaEntrada
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

	SET NOCOUNT OFF;
END

GO
