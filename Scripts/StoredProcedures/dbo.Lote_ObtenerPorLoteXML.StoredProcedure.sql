USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerPorLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorLoteXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Gilberto Carranza
-- Create date: 2014-12-02
-- Description:	Obtiene un Corral si tiene existencia
-- Lote_ObtenerPorLoteXML
--=============================================
CREATE PROCEDURE [dbo].[Lote_ObtenerPorLoteXML]
@Lotes	XML
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		C.CorralID,
		C.OrganizacionID,
		C.Codigo,
		C.TipoCorralID,
		C.Capacidad,
		C.MetrosLargo,
		C.MetrosAncho,
		C.Seccion,
		C.Orden,
		C.Activo,
		C.FechaCreacion,
		C.UsuarioCreacionID
		, O.Descripcion			AS Organizacion
		, TC.Descripcion		AS TipoCorral
		, L.Lote
		, L.LoteID
		, L.Activo				AS LoteActivo
		, L.Cabezas
		, L.CabezasInicio
		, GC.GrupoCorralID
		, GC.Descripcion		AS GrupoCorral
	FROM Corral C
	INNER JOIN Lote L
		ON (C.CorralID = L.CorralID)
	INNER JOIN  Organizacion O
		ON (C.OrganizacionID = O.OrganizacionID)
	INNER JOIN  TipoCorral TC
		ON (C.TipoCorralID = TC.TipoCorralID)
	INNER JOIN  GrupoCorral GC
		ON (TC.GrupoCorralID = GC.GrupoCorralID)
	INNER JOIN 
	(
		SELECT T.item.value('./LoteID[1]', 'INT') AS LoteID
		FROM @Lotes.nodes('ROOT/Lotes') AS T(item)
	) x ON (L.LoteID = x.LoteID)
	GROUP BY C.CorralID,
		C.OrganizacionID,
		C.Codigo,
		C.TipoCorralID,
		C.Capacidad,
		C.MetrosLargo,
		C.MetrosAncho,
		C.Seccion,
		C.Orden,
		C.Activo,
		C.FechaCreacion,
		C.UsuarioCreacionID
		, O.Descripcion
		, TC.Descripcion
		, L.Lote
		, L.LoteID
		, L.Activo
		, L.Cabezas
		, L.CabezasInicio
		, GC.GrupoCorralID
		, GC.Descripcion
	SET NOCOUNT OFF;
END

GO
