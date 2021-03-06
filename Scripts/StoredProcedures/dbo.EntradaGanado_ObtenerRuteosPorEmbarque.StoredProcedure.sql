USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerRuteosPorEmbarque]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerRuteosPorEmbarque]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerRuteosPorEmbarque]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 03/08/2015
-- Description:	Obtiene las partidas de Ruteo por embarque
-- EntradaGanado_ObtenerRuteosPorEmbarque 7126
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerRuteosPorEmbarque] 
	@EmbarqueID INT	
AS 
SELECT
	eg.OrganizacionID,
	eg.FolioEntrada,
	eg.FechaEntrada,
	eg.CabezasRecibidas,
	COUNT(DISTINCT a.AnimalID) AS CabezasCortadas,
	ISNULL(ah.Cabezas, 0) AS CabezasHistorico,
	COUNT(DISTINCT a.AnimalID) + ISNULL(ah.Cabezas, 0) AS CabezasTotal,
	eg.CabezasRecibidas - (COUNT(DISTINCT a.AnimalID) + ISNULL(ah.Cabezas, 0)) CabezasPendientes,
	l.Cabezas AS CabezasLote,
	eg.EmbarqueID
FROM EntradaGanado eg (NOLOCK)
INNER JOIN Organizacion o (NOLOCK)	ON o.OrganizacionID = eg.OrganizacionOrigenID
LEFT JOIN Animal a (NOLOCK)	ON a.OrganizacionIDEntrada = eg.OrganizacionID	AND a.FolioEntrada = eg.FolioEntrada
INNER JOIN EntradaGanadoCosteo egc (NOLOCK)	ON egc.EntradaGanadoID = eg.EntradaGanadoID	AND egc.Activo = 1
INNER JOIN EntradaGanadoCosto egc2 (NOLOCK)	ON egc2.EntradaGanadoCosteoID = egc.EntradaGanadoCosteoID	AND egc2.CostoID = 1
INNER JOIN Lote l (NOLOCK)	ON l.LoteID = eg.LoteID
INNER JOIN Corral c (NOLOCK)	ON c.CorralID = eg.CorralID
LEFT JOIN (SELECT
	ah.OrganizacionIDEntrada,
	ah.FolioEntrada,
	COUNT(DISTINCT ah.AnimalID) AS Cabezas
			FROM AnimalHistorico ah (NOLOCK)
			GROUP BY	ah.OrganizacionIDEntrada,
			ah.FolioEntrada) ah	ON ah.OrganizacionIDEntrada = eg.OrganizacionID	AND ah.FolioEntrada = eg.FolioEntrada
WHERE eg.EmbarqueID = @EmbarqueID
GROUP BY	eg.OrganizacionID,
			eg.FolioEntrada,
			eg.FechaEntrada,
			eg.CabezasRecibidas,
			l.Cabezas,
			ah.Cabezas,
			eg.EmbarqueID

GO
