USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerPorLotesXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerPorLotesXML]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerPorLotesXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 16/01/2015
-- Description:  Obtiene animales por su disponibilidad
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerPorLotesXML]
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
	FROM Animal a(NOLOCK)
	INNER JOIN AnimalMovimiento am(NOLOCK) ON a.AnimalID = am.AnimalID and am.Activo = 1
	INNER JOIN #tLote tL
		ON (AM.LoteID = tL.LoteID)
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

	SET NOCOUNT OFF;
END

GO
