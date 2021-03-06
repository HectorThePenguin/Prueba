USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_Programadas2]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_Programadas2]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_Programadas2]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		Noel.Gerardo
-- Fecha: 10-12-2013
-- Descripción:	Obtiene entradas de ganado evaluadas 
-- EntradaGanado_Programadas2 1, 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_Programadas2]
	@FolioEntrada INT, 
	@OrganizacionID INT
AS
BEGIN
if @FolioEntrada < 0
BEGIN
SET @FolioEntrada = NULL
END
	SELECT 
		eg.EntradaGanadoID,
		FolioEntrada,
		eg.OrganizacionID,
		o.Descripcion [Organizacion],
		too.TipoOrganizacionID, 
		too.Descripcion [TipoOrganizacion],
		OrganizacionOrigenID,
		FechaEntrada,
		EmbarqueID,
		FolioOrigen,
		eg.FechaSalida,
		eg.CamionID,
		ChoferID,
		JaulaID,
		/*EG.CabezasOrigen,*/
		CabezasRecibidas AS [CabezasOrigen],
		CabezasRecibidas,
		eg.OperadorID,
		PesoBruto,
		PesoTara,
		EsRuteo,
		Fleje,
		CheckList,
		eg.CorralID,
		co.Codigo [Corral],
		eg.LoteID,
		lo.Lote,
		eg.Observacion,
		ImpresionTicket,
		Costeado,
		Manejado,
		eg.Activo,
		eva.FechaEvaluacion,
		CAST(CASE 
			 WHEN eva.EsMetafilaxia = 1 THEN eva.EsMetafilaxia
			 WHEN eva.MetafilaxiaAutorizada = 1 THEN eva.MetafilaxiaAutorizada
			 ELSE 0
		END AS BIT) AS EsMetafilaxia,
		CAST(SUM(PesoOrigen) AS INT) [PesoOrigen],
		CAST(SUM(PesoLlegada) AS INT) [PesoLlegada],
		eva.NivelGarrapata
	 FROM EntradaGanado eg
	 INNER JOIN Organizacion o(NOLOCK) ON eg.OrganizacionOrigenID = o.OrganizacionID
	 INNER JOIN TipoOrganizacion too(NOLOCK) ON o.TipoOrganizacionID = too.TipoOrganizacionID
	 INNER JOIN Corral co(NOLOCK) ON eg.CorralID = co.CorralID
	 INNER JOIN Lote lo(NOLOCK) ON eg.LoteID = lo.LoteID /*AND lo.Activo = 1*/
	 INNER JOIN EvaluacionCorral eva(NOLOCK) ON eva.LoteID = lo.LoteID
	 INNER JOIN ProgramacionCorte p(NOLOCK) ON p.FolioEntradaID = eg.FolioEntrada
	 LEFT JOIN EntradaCondicion EC (NOLOCK) ON eg.EntradaGanadoID = EC.EntradaGanadoID AND EC.CondicionID = 5
	INNER JOIN EntradaGanadoCosteo EGC (NOLOCK) ON EGC.EntradaGanadoID = eg.EntradaGanadoID AND EGC.Activo = 1
	INNER JOIN EntradaDetalle et (NOLOCK) ON EGC.EntradaGanadoCosteoID = et.EntradaGanadoCosteoID
     /*INNER JOIN EntradaDetalle et (NOLOCK) ON et.EntradaGanadoID =  eg.EntradaGanadoID*/
	WHERE eg.OrganizacionID = @OrganizacionID
	AND  FolioEntrada = COALESCE(@FolioEntrada, FolioEntrada)
	AND eg.Activo=1
	AND p.Activo=1
  GROUP BY eg.EntradaGanadoID,
		FolioEntrada,
		eg.OrganizacionID,
		o.Descripcion,
		too.TipoOrganizacionID, 
		too.Descripcion,
		OrganizacionOrigenID,
		FechaEntrada,
		EmbarqueID,
		FolioOrigen,
		eg.FechaSalida,
		eg.CamionID,
		ChoferID,
		JaulaID,
		CabezasOrigen,
		CabezasRecibidas,
		eg.OperadorID,
		PesoBruto,
		PesoTara,
		EsRuteo,
		Fleje,
		CheckList,
		eg.CorralID,
		co.Codigo,
		eg.LoteID,
		lo.Lote,
		eg.Observacion,
		ImpresionTicket,
		Costeado,
		Manejado,
		eg.Activo,
		eva.FechaEvaluacion,
		eva.EsMetafilaxia,
	    eva.MetafilaxiaAutorizada,			
		/*PesoOrigen,*/
		PesoLlegada,
		lo.Cabezas,
		NivelGarrapata
		ORDER BY EsMetaFilaxia DESC, eva.FechaEvaluacion
END

GO
