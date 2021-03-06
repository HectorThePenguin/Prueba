USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorFolioEntradaOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPorFolioEntradaOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorFolioEntradaOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 08-04-2014
-- Description:	Obtiene una entrada de ganado por ID 
-- EntradaGanado_ObtenerPorFolioEntradaOrganizacion 5, 4
-- Modificado
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPorFolioEntradaOrganizacion]
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
		eg.CamionID,
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
	 FROM EntradaGanado eg
	 INNER JOIN Organizacion o 
		ON eg.OrganizacionOrigenID = o.OrganizacionID
	 INNER JOIN TipoOrganizacion TOO 
		ON o.TipoOrganizacionID = too.TipoOrganizacionID
	 LEFT JOIN Corral co 
		ON eg.CorralID = co.CorralID
	 LEFT JOIN Lote lo 
		ON eg.LoteID = lo.LoteID
	WHERE FolioEntrada = @FolioEntrada
		AND eg.Activo = 1
		AND eg.OrganizacionID = @OrganizacionID
END

GO
