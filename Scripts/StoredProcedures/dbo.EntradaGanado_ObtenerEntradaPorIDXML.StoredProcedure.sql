USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradaPorIDXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerEntradaPorIDXML]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradaPorIDXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 13-11-2014
-- Description:	Obtiene una entrada de ganado por ID
-- EntradaGanado_ObtenerEntradaPorIDXML
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerEntradaPorIDXML]
@XmlEntradaGanadoID	XML
AS
BEGIN	
	SET NOCOUNT ON
			SELECT 
			EG.EntradaGanadoID,
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
		 FROM 
		 (
			SELECT T.N.value('./EntradaGanadoID[1]','INT') AS EntradaGanadoID
			FROM @XmlEntradaGanadoID.nodes('/ROOT/EntradaGanado') as T(N)
		 ) A
		 INNER JOIN EntradaGanado eg
			ON (A.EntradaGanadoID = eg.EntradaGanadoID)
		 INNER JOIN Organizacion o on eg.OrganizacionOrigenID = o.OrganizacionID
		 INNER JOIN TipoOrganizacion too on o.TipoOrganizacionID = too.TipoOrganizacionID
		 LEFT JOIN Corral co ON eg.CorralID = co.CorralID
		 LEFT JOIN Lote lo ON eg.LoteID = lo.LoteID
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
		INNER JOIN (
			SELECT T.N.value('./EntradaGanadoID[1]','INT') AS EntradaGanadoID
			FROM @XmlEntradaGanadoID.nodes('/ROOT/EntradaGanado') as T(N)
		 ) A ON (EC.EntradaGanadoID = A.EntradaGanadoID)
	SET NOCOUNT OFF
END

GO
