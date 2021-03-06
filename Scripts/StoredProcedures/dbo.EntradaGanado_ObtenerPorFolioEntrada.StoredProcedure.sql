USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorFolioEntrada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPorFolioEntrada]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorFolioEntrada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 30-10-2013
-- Description:	Obtiene una entrada de ganado por ID 
-- EntradaGanado_ObtenerPorFolioEntrada 1000, 1
-- Modificado
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPorFolioEntrada]
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
	 FROM EntradaGanado eg
	 INNER JOIN Organizacion o 
		ON eg.OrganizacionOrigenID = o.OrganizacionID
	 INNER JOIN TipoOrganizacion TOO 
		ON o.TipoOrganizacionID = too.TipoOrganizacionID
	 INNER JOIN EmbarqueDetalle ED  
		ON ED.EmbarqueID = EG.EmbarqueID 
		AND ED.OrganizacionOrigenID = EG.OrganizacionOrigenID
		AND ED.Recibido = 0
	 LEFT JOIN Corral co 
		ON eg.CorralID = co.CorralID
	 LEFT JOIN Lote lo 
		ON eg.LoteID = lo.LoteID
	WHERE FolioEntrada = @FolioEntrada
		AND eg.Activo = 1
		AND eg.OrganizacionID = @OrganizacionID
	SELECT 
		EC.EntradaCondicionID,
		EC.EntradaGanadoID,
		EC.CondicionID,
		EC.Cabezas,
		EC.Activo,
		C.Descripcion
	FROM EntradaCondicion EC
	INNER JOIN EntradaGanado EG
		ON EC.EntradaGanadoID = EG.EntradaGanadoID
	INNER JOIN Condicion C
		ON (EC.CondicionID = C.CondicionID)
	WHERE EG.FolioEntrada = @FolioEntrada
	AND EG.OrganizacionID = @OrganizacionID
	SELECT CE.CostoID, CE.Importe FROM EntradaGanado EG 
	INNER JOIN Embarque E ON EG.EmbarqueID = E.EmbarqueID
	INNER JOIN EmbarqueDetalle ED ON E.EmbarqueID = ED.EmbarqueID
	INNER JOIN CostoEmbarqueDetalle CE ON CE.EmbarqueDetalleID = ED.EmbarqueDetalleID
	WHERE EG.FolioEntrada = @FolioEntrada AND EG.OrganizacionID = @OrganizacionID
END

GO
