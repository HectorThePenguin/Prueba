USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorCorralID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerPorCorralID]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorCorralID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/************************************
Autor	  : Edgar.Villarreal
Fecha	  : 20/02/2014
Proposito : Obtiene un Lote por ID
************************************/
CREATE PROCEDURE [dbo].[Lote_ObtenerPorCorralID]
@CorralID INT
, @OrganizacionID INT
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
		AND L.CorralID = @CorralID
		AND L.Activo = 1
	SET NOCOUNT OFF
END

GO
