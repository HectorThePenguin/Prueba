USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorFolioEntradaCortadaIncompleta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPorFolioEntradaCortadaIncompleta]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorFolioEntradaCortadaIncompleta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 16/06/2015
-- Description:	Obtiene la Entrada de Ganado por Folio, y que no haya sido Cortada (Manejado = 0)
-- EntradaGanado_ObtenerPorFolioEntradaCortadaIncompleta 1000, 1
-- Modificado
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPorFolioEntradaCortadaIncompleta]
	@FolioEntrada INT, 
	@OrganizacionID INT
AS 
BEGIN	
	SELECT 
		EG.EntradaGanadoID,
		EG.FolioEntrada,
		EG.OrganizacionID,
		O.Descripcion [Organizacion],
		TOO.TipoOrganizacionID, 
		TOO.Descripcion [TipoOrganizacion],
		EG.OrganizacionOrigenID,
		EG.FechaEntrada,
		EG.EmbarqueID,
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
		CO.Codigo [Corral],
		EG.LoteID,
		LO.Lote,
		EG.Observacion,
		EG.ImpresionTicket,
		EG.Costeado,
		EG.Manejado,
		EG.Activo
		, '' AS NombreOperador	
		, 0 AS ProveedorID
		, '' AS Proveedor
		, EG.Guia
		, EG.Factura
		, EG.Poliza
		, EG.HojaEmbarque
		, EG.ManejoSinEstres
		, EG.CertificadoZoosanitario
		, EG.PruebaTB
		, EG.PruebaTR
		, EG.CondicionJaulaID
	 FROM EntradaGanado eg (nolock)
	 INNER JOIN Organizacion o  (nolock)
		ON eg.OrganizacionOrigenID = o.OrganizacionID
	 INNER JOIN TipoOrganizacion TOO  (nolock)
		ON o.TipoOrganizacionID = too.TipoOrganizacionID	 
	 LEFT JOIN Corral co  (nolock)
		ON eg.CorralID = co.CorralID
	 LEFT JOIN Lote lo (nolock)
		ON eg.LoteID = lo.LoteID
	WHERE FolioEntrada = @FolioEntrada
		AND eg.Activo = 1
		AND eg.OrganizacionID = @OrganizacionID
		AND eg.Manejado = 0
		AND EG.Costeado = 1

	SELECT 
		EC.EntradaCondicionID,
		EC.EntradaGanadoID,
		EC.CondicionID,
		EC.Cabezas,
		EC.Activo,
		C.Descripcion
	FROM EntradaCondicion EC (nolock)
	INNER JOIN EntradaGanado EG (nolock)
		ON EC.EntradaGanadoID = EG.EntradaGanadoID
	INNER JOIN Condicion C (nolock)
		ON (EC.CondicionID = C.CondicionID)
	WHERE EG.FolioEntrada = @FolioEntrada
	AND EG.OrganizacionID = @OrganizacionID
END

GO
