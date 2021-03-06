USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorCorralIdOrganizacionID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerPorCorralIdOrganizacionID]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorCorralIdOrganizacionID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/************************************
Autor	  : Gilberto Carranza
Fecha	  : 20/11/2013
Proposito : Obtiene un Lote por ID
************************************/
CREATE PROCEDURE [dbo].[Lote_ObtenerPorCorralIdOrganizacionID]
@CorralID INT
, @OrganizacionID INT
, @EmbarqueID INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT L.LoteID
			,L.OrganizacionID
			,L.CorralID
			,L.TipoCorralID
			,L.TipoProcesoID
			,L.FechaInicio
			,L.CabezasInicio
			,L.FechaCierre
			,L.Cabezas
			,L.FechaDisponibilidad
			,L.DisponibilidadManual
			,L.Activo
			,L.FechaCreacion
			,L.UsuarioCreacionID
			,L.FechaModificacion
			,L.UsuarioModificacionID
			, L.Lote
	FROM Lote L
	INNER JOIN EntradaGanado EG
		ON (L.LoteID = EG.LoteID
			AND EG.EmbarqueID = @EmbarqueID)
	WHERE L.OrganizacionID = @OrganizacionID
		AND L.CorralID = @CorralID
	SET NOCOUNT OFF
END

GO
