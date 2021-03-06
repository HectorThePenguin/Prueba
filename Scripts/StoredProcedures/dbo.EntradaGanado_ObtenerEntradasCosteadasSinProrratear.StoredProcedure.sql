USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradasCosteadasSinProrratear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerEntradasCosteadasSinProrratear]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradasCosteadasSinProrratear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    César Valdez Figueroa
-- Create date: 03/04/2014
-- Description:  Obtener Entradas costeadas sin prorratear
-- Origen: APInterfaces
-- EntradaGanado_ObtenerEntradasCosteadasSinProrratear 2
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerEntradasCosteadasSinProrratear]
	@TipoOrganizacion INT
AS
  BEGIN
    SET NOCOUNT ON

	SELECT EG.EntradaGanadoID
		,EG.LoteID
		,L.CabezasInicio
		,EG.FolioEntrada
		,dbo.ObtenerPartidasCorralLote(EG.CorralID, EG.LoteID, '|') "FolioEntradaAgrupado"
		,EG.FolioOrigen
		,EG.OrganizacionOrigenID
		,EG.OrganizacionID
		,EG.CabezasRecibidas
		,EG.CabezasOrigen
		,Origen.TipoOrganizacionID
	FROM EntradaGanado EG
	INNER JOIN EntradaGanadoCosteo EGC ON (
			EGC.EntradaGanadoID = EG.EntradaGanadoID
			AND EGC.Activo = 1
			)
	INNER JOIN Lote L ON (L.LoteID = EG.LoteID AND L.Activo = 0)
	INNER JOIN Organizacion O ON (
			EG.OrganizacionID = O.OrganizacionID
			AND O.Activo = 1
			)
	INNER JOIN Organizacion Origen ON (EG.OrganizacionOrigenID = Origen.OrganizacionID)
	WHERE EG.Costeado = 1
		AND EGC.Prorrateado = 0
		AND O.TipoOrganizacionID = @TipoOrganizacion
		AND EG.Activo = 1
	
	END

GO
