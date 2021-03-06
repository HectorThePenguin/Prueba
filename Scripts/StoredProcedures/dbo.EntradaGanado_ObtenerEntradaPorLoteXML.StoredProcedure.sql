USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradaPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerEntradaPorLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradaPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranzas
-- Create date: 22-08-2014
-- Description:	Obtiene una entrada de ganado por loteID corralID y organizacionID 
-- EntradaGanado_ObtenerEntradaPorLoteXML
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerEntradaPorLoteXML]
@OrganizacionID		INT
, @XmlCorralLote	XML
AS
BEGIN	
	SET NOCOUNT ON
		SELECT eng.FolioEntrada
			 , eng.OrganizacionOrigenID
			 , O.TipoOrganizacionID
			 , eng.PesoBruto
			 , eng.PesoTara
			 , eng.CabezasRecibidas
			 , eng.LoteID
			 , eng.CorralID
		FROM EntradaGanado (NOLOCK) AS eng 
		INNER JOIN Organizacion (NOLOCK) AS O 
			ON (O.OrganizacionID = eng.OrganizacionOrigenID 
				AND eng.OrganizacionID = @OrganizacionID 
				AND eng.Activo = 1
				AND O.Activo = 1)
		INNER JOIN 
		(
			SELECT T.N.value('./CorralID[1]','INT') AS CorralID
				,  T.N.value('./LoteID[1]','INT') AS LoteID
			FROM @XmlCorralLote.nodes('/ROOT/CorralesLotes') as T(N)
		) A
			ON (eng.LoteID = A.LoteID 
				AND eng.CorralID = A.CorralID)
	SET NOCOUNT OFF
END

GO
