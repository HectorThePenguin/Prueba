USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 30-10-2013
-- Description:	Obtiene una entrada de ganado por ID 
-- EntradaGanado_ObtenerPorID 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPorID]
	@EntradaGanadoID INT
AS 
BEGIN	
	SELECT 
		EntradaGanadoID,
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
		CabezasOrigen,
		CabezasRecibidas,
		OperadorID,
		PesoBruto,
		PesoTara,
		EsRuteo,
		Fleje,
		CheckList,
		eg.CorralID,
		co.Codigo [Corral],
		eg.LoteID,
		lo.Lote,
		Observacion,
		ImpresionTicket,
		Costeado,
		Manejado,
		eg.Activo		
		, EG.CertificadoZoosanitario
		, EG.PruebaTB
		, EG.PruebaTR
		, EG.CondicionJaulaID
	 FROM EntradaGanado eg
	 INNER JOIN Organizacion o on eg.OrganizacionOrigenID = o.OrganizacionID
	 INNER JOIN TipoOrganizacion too on o.TipoOrganizacionID = too.TipoOrganizacionID
	 LEFT JOIN Corral co ON eg.CorralID = co.CorralID
	 LEFT JOIN Lote lo ON eg.LoteID = lo.LoteID
	WHERE EntradaGanadoID = @EntradaGanadoID
	SELECT 
		EC.EntradaCondicionID,
		EC.EntradaGanadoID,
		EC.CondicionID,
		EC.Cabezas,
		EC.Activo,
		C.Descripcion
	FROM EntradaCondicion EC
	INNER JOIN Condicion C
		ON (EC.CondicionID = C.CondicionID)
	WHERE EntradaGanadoID = @EntradaGanadoID
END

GO
