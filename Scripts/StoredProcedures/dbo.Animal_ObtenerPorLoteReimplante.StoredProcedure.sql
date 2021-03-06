USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerPorLoteReimplante]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerPorLoteReimplante]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerPorLoteReimplante]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 12/09/2014
-- Description:  Obtiene animales por su de los lotes de reimplante
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerPorLoteReimplante]
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
	INNER JOIN Lote lo 
		ON L.LoteID = lo.LoteID
	WHERE lo.OrganizacionID = @OrganizacionID	
		and am.Activo = 1
		and lo.Activo = 1
		and am.TipoMovimientoID = 6 --Movimiento de Reimplante

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
