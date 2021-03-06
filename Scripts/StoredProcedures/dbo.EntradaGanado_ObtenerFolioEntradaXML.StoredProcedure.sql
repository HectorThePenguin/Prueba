USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerFolioEntradaXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerFolioEntradaXML]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerFolioEntradaXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 04-12-2014
-- Description:	Obtiene una entrada de ganado por ID
-- EntradaGanado_ObtenerFolioEntradaXML
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerFolioEntradaXML]
@XmlFolioEntrada	XML
AS
BEGIN	

	SET NOCOUNT ON

			SELECT 
			EG.EntradaGanadoID,
			EG.FolioEntrada,
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
		 FROM 
		 (
			SELECT T.N.value('./FolioEntrada[1]','INT') AS FolioEntrada
				,  T.N.value('./OrganizacionID[1]','INT') AS OrganizacionID
			FROM @XmlFolioEntrada.nodes('/ROOT/EntradaGanado') as T(N)
		 ) A
		 INNER JOIN EntradaGanado eg
			ON (A.FolioEntrada = eg.FolioEntrada
				AND A.OrganizacionID = EG.OrganizacionID)
		 INNER JOIN Organizacion o on eg.OrganizacionOrigenID = o.OrganizacionID
		 INNER JOIN TipoOrganizacion too on o.TipoOrganizacionID = too.TipoOrganizacionID
		 LEFT JOIN Corral co ON eg.CorralID = co.CorralID
		 LEFT JOIN Lote lo ON eg.LoteID = lo.LoteID
		 GROUP BY EG.EntradaGanadoID,
			EG.FolioEntrada,
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
			OperadorID,
			PesoBruto,
			PesoTara,
			EsRuteo,
			Fleje,
			CheckList,
			eg.CorralID,
			co.Codigo,
			eg.LoteID,
			lo.Lote,
			Observacion,
			ImpresionTicket,
			Costeado,
			Manejado,
			eg.Activo		

	SET NOCOUNT OFF
END
GO
