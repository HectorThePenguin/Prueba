USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerPorCorral]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorCorral]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cesar.Valdez
-- Fecha: 2013-12-19
-- Origen: APInterfaces
-- Descripci�n:	Obtiene el lote del corral
-- EXEC Lote_ObtenerPorCorral 1,1
-- =============================================
CREATE PROCEDURE [dbo].[Lote_ObtenerPorCorral]
	@OrganizacionID INT, 
	@CorralID INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT TOP 1 L.LoteID,
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
			L.FechaCreacion,
			L.UsuarioCreacionID,
			L.FechaModificacion,
			L.UsuarioModificacionID,
			L.Lote
	 FROM Lote L
	INNER JOIN Corral C ON C.CorralID = L.CorralID
	WHERE C.OrganizacionID = @OrganizacionID
	  AND C.CorralID = @CorralID
	  AND C.Capacidad > L.Cabezas
	  AND C.Activo = 1
	  AND L.FechaCierre IS NULL
	  AND L.Activo = 1
	SET NOCOUNT OFF
END

GO
