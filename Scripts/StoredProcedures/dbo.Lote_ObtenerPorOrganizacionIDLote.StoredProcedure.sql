USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorOrganizacionIDLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerPorOrganizacionIDLote]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorOrganizacionIDLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/************************************
Autor	  : Gilberto Carranza
Fecha	  : 28/11/2013
Proposito : Obtiene un Lote por Organizacion ID
			y Lote
[Lote_ObtenerPorOrganizacionIDLote] 1,3
************************************/
CREATE PROCEDURE [dbo].[Lote_ObtenerPorOrganizacionIDLote]
@OrganizacionID INT
, @Lote VARCHAR(250)
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
	WHERE L.OrganizacionID = @OrganizacionID
		AND L.Lote = @Lote
	SET NOCOUNT OFF
END

GO
