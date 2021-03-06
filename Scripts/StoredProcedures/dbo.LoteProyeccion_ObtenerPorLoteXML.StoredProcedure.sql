USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_ObtenerPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteProyeccion_ObtenerPorLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_ObtenerPorLoteXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 22-08-2014
-- Description:	Obtiene una entrada de ganado por loteID corralID y organizacionID 
-- LoteProyeccion_ObtenerPorLoteXML
-- =============================================
CREATE PROCEDURE [dbo].[LoteProyeccion_ObtenerPorLoteXML]
@OrganizacionID		INT
, @XmlLote			XML
AS
BEGIN	
	SET NOCOUNT ON
		SELECT
			LP.LoteProyeccionID,
			LP.LoteID,
			LP.OrganizacionID,
			LP.Frame,
			LP.GananciaDiaria,
			LP.ConsumoBaseHumeda,
			LP.Conversion,
			LP.PesoMaduro,
			LP.PesoSacrificio,
			LP.DiasEngorda,
			LP.FechaEntradaZilmax
		FROM LoteProyeccion LP
		INNER JOIN 
		(
			SELECT T.N.value('./LoteID[1]','INT') AS LoteID
			FROM @XmlLote.nodes('/ROOT/Lotes') as T(N)
		) A
			ON (LP.LoteID = A.LoteID
				AND LP.OrganizacionID = @OrganizacionID)
	SET NOCOUNT OFF
END

GO
