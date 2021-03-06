USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorTipoCorralID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPorTipoCorralID]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorTipoCorralID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Marco Zamora
-- Origen:        AP Interfaces
-- Create date: 25/11/2013
-- Description:  Metodo para Obtener las entradas de Ganado por Tipo de Corral
-- EXEC EntradaGanado_ObtenerPorTipoCorralID 1, 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPorTipoCorralID]
 @OrganizacionID INT,
 @TipoCorralID INT
AS
BEGIN
    SELECT
		EG.EntradaGanadoID,
		EG.FolioEntrada,
		EG.OrganizacionID,
		EG.OrganizacionOrigenID,
		EG.FechaEntrada,
		EG.FolioOrigen,
		EG.FechaSalida,
		EG.CamionID,
		EG.ChoferID,
		EG.JaulaID,
		EG.CabezasOrigen,
		EG.CabezasRecibidas,
		EG.OperadorID,
		EG.PesoBruto,
		EG.PesoTara,
		EG.EsRuteo,
		EG.Fleje,
		EG.CheckList,
		EG.CorralID,
		EG.LoteID,
		EG.Observacion,
		EG.ImpresionTicket,
		EG.Costeado,
		EG.Manejado,
		EG.Activo,
		C.Codigo CodigoCorral,
		O.Descripcion OrganizacionOrigen,
		OP.Nombre + ' ' + OP.ApellidoPaterno + ' ' + OP.ApellidoMaterno Evaluador,
		EVC.FechaEvaluacion, 
		CASE 
			 WHEN EVC.EsMetafilaxia =  1 THEN EVC.EsMetafilaxia
			 WHEN EVC.MetafilaxiaAutorizada =  1 THEN EVC.MetafilaxiaAutorizada
			 ELSE 0
		END AS EsMetafilaxiaOAutorizada,
		SUM(CASE WHEN TG.Sexo = 'M' THEN ED.Cabezas ELSE 0 END) AS Machos,
		SUM(CASE WHEN TG.Sexo = 'H' THEN ED.Cabezas ELSE 0 END) AS Hembras,
		(CASE WHEN EC.CondicionID = 5 THEN EC.Cabezas ELSE 0 END) AS Rechazos,
		/* Si es compra directa se vaa entrada detalle sino a interfazSalidaAnimal*/
		CASE WHEN O.TipoOrganizacionID = 3
		THEN
			CAST(COALESCE((	
				SELECT SUM(ED.PesoOrigen)
					FROM EntradaDetalle ED
				 INNER JOIN EntradaGanadoCosteo EGC ON EGC.EntradaGanadoCosteoID = ED.EntradaGanadoCosteoID 
				 WHERE EGC.EntradaGanadoID = eg.EntradaGanadoID
			 ), 0 ) AS INT)
		ELSE
			CAST(COALESCE((	
				SELECT SUM(PesoOrigen) 
					FROM InterfaceSalidaAnimal 
					 WHERE OrganizacionID = eg.OrganizacionOrigenID
						 AND SalidaID = eg.FolioOrigen
			 ), 0 ) AS INT)
		END AS PesoOrigen
		,EVC.NivelGarrapata
	FROM EntradaGanado EG (NOLOCK)
	INNER JOIN Lote L (NOLOCK) ON EG.LoteID = L.LoteID
	INNER JOIN Corral C (NOLOCK) ON C.CorralID = L.CorralID
	INNER JOIN Organizacion O (NOLOCK) ON O.OrganizacionID = EG.OrganizacionOrigenID
	INNER JOIN EvaluacionCorral EVC( NOLOCK) ON EVC.LoteID = l.LoteID
	INNER JOIN Operador OP (NOLOCK) ON OP.OperadorID = EVC.OperadorID
	LEFT JOIN EntradaCondicion EC (NOLOCK) ON EG.EntradaGanadoID = EC.EntradaGanadoID AND EC.CondicionID = 5
	INNER JOIN EntradaGanadoCosteo EGC (NOLOCK) ON EGC.EntradaGanadoID = EG.EntradaGanadoID AND EGC.Activo = 1
	INNER JOIN EntradaDetalle ED (NOLOCK) ON EGC.EntradaGanadoCosteoID = ED.EntradaGanadoCosteoID
	INNER JOIN TipoGanado TG (NOLOCK) ON TG.TipoGanadoID = ED.TipoGanadoID
	LEFT JOIN ProgramacionCorte PC(NOLOCK) ON PC.FolioEntradaID = EG.FolioEntrada AND PC.OrganizacionID = EG.OrganizacionID
	WHERE EG.OrganizacionID = @OrganizacionID
	  AND L.Activo = 1
	  AND C.TipoCorralID = @TipoCorralID
	  AND PC.FolioProgramacionID IS NULL
	GROUP BY
		EG.EntradaGanadoID,
		EG.FolioEntrada,
		EG.OrganizacionID,
		EG.OrganizacionOrigenID,
		EG.FechaEntrada,
		EG.FolioOrigen,
		EG.FechaSalida,
		EG.CamionID,
		EG.ChoferID,
		EG.JaulaID,
		EG.CabezasOrigen,
		EG.CabezasRecibidas,
		EG.OperadorID,
		EG.PesoBruto,
		EG.PesoTara,
		EG.EsRuteo,
		EG.Fleje,
		EG.CheckList,
		EG.CorralID,
		EG.LoteID,
		EG.Observacion,
		EG.ImpresionTicket,
		EG.Costeado,
		EG.Manejado,
		EG.Activo,
		C.Codigo,
		O.Descripcion,
		OP.Nombre,
		OP.ApellidoPaterno,
		OP.ApellidoMaterno,
		EVC.FechaEvaluacion, 
		EVC.EsMetafilaxia,
	    EVC.MetafilaxiaAutorizada,
		O.TipoOrganizacionID,
		EC.CondicionID,
		EC.Cabezas
		,EVC.NivelGarrapata
	ORDER BY EsMetafilaxiaOAutorizada DESC, EVC.FechaEvaluacion
END

GO
