USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerLotesActivos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerLotesActivos]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerLotesActivos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--************************************
--Autor	  : edgar.villarreal
--Fecha	  : 19/02/2014
--Proposito : Obtener Lote Por Corral que estan activos
--Exec Lote_ObtenerLotesActivos 4, 1
--************************************
CREATE PROCEDURE [dbo].[Lote_ObtenerLotesActivos]
	@OrganizacionID INT,
	@CorralID INT,
	@Activo INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT top 1
			L.LoteID
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
			,L.Lote
	FROM Lote L
	INNER JOIN Corral AS C ON C.CorralID=L.CorralID
	WHERE L.OrganizacionID = @OrganizacionID
		AND C.CorralID = @CorralID		
		AND L.Activo = @Activo
		AND C.Activo = @Activo;
END

GO
