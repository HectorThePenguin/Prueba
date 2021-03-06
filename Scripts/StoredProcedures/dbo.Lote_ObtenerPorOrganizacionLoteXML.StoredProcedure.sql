USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorOrganizacionLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerPorOrganizacionLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorOrganizacionLoteXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 22/08/2014
-- Description: 
-- SpName     : Lote_ObtenerPorOrganizacionLoteXML 1, '<ROOT><Corrales><CorralID>1</CorralID></Corrales></ROOT>'
--======================================================
CREATE PROCEDURE [dbo].[Lote_ObtenerPorOrganizacionLoteXML]
@OrganizacionID	INT
, @XmlCorral XML
AS
BEGIN
	SET NOCOUNT ON
		SELECT L.LoteID,
			L.OrganizacionID,
			L.CorralID,
			L.TipoCorralID,
			L.TipoProcesoID,
			L.FechaInicio,
			L.CabezasInicio,
			L.FechaCierre,
			L.Cabezas,
			L.FechaDisponibilidad,
			L.DisponibilidadManual,
			L.Activo,
			L.Lote,
			L.FechaCreacion
		FROM
		(
			SELECT T.N.value('./CorralID[1]','INT') AS CorralID
			FROM @XmlCorral.nodes('/ROOT/Corrales') as T(N)
		) A
		INNER JOIN Corral C
			ON (A.CorralID = C.CorralID
				AND C.OrganizacionID = @OrganizacionID
				AND C.Activo = 1)
		INNER JOIN Lote L
			ON (C.CorralID = L.CorralID
				AND L.Activo = 1
				AND L.OrganizacionID = @OrganizacionID)
	SET NOCOUNT OFF
END

GO
