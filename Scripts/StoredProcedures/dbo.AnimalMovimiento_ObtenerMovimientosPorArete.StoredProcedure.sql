USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerMovimientosPorArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_ObtenerMovimientosPorArete]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerMovimientosPorArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Jorge Luis Velazquez Araujo
-- Create date: 04/11/2014
-- Description:  Obtiene los movimientos de un animal en base a su arete
-- AnimalMovimiento_ObtenerMovimientosPorArete '1'
-- =============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_ObtenerMovimientosPorArete] @OrganizacionID INT
	,@Arete VARCHAR(15)
AS
BEGIN
	SET NOCOUNT ON

	SELECT am.AnimalID
		,AnimalMovimientoID
		,am.OrganizacionID
		,am.CorralID
		,lo.LoteID
		,tc.TipoCorralID
		,gc.GrupoCorralID
		,FechaMovimiento
		,Peso
		,Temperatura
		,TipoMovimientoID
		,TrampaID
		,OperadorID
		,Observaciones
		,LoteIDOrigen
		,AnimalMovimientoIDAnterior
		,am.Activo
	FROM AnimalMovimiento am(NOLOCK)
	inner join Animal a(NOLOCK) on a.AnimalID = am.AnimalID
	inner join Lote lo on am.LoteID = lo.LoteID
	inner join TipoCorral tc on lo.TipoCorralID = tc.TipoCorralID
	inner join GrupoCorral gc on tc.GrupoCorralID = gc.GrupoCorralID
	where am.OrganizacionID = @OrganizacionID
	and a.Arete = @Arete

	SET NOCOUNT OFF
END

GO
