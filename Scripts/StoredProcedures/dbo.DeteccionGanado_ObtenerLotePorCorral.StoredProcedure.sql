USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerLotePorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ObtenerLotePorCorral]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerLotePorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Pedro.Delgado
-- Fecha: 2014-02-28
-- Origen: APInterfaces
-- Descripci�n:	Obtiene el lote del corral
-- EXEC Lote_ObtenerPorCorral 1,1
-- =============================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ObtenerLotePorCorral]
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
			L.Lote,
			C.Capacidad
	 FROM Lote L
	INNER JOIN Corral C ON C.CorralID = L.CorralID
	WHERE C.OrganizacionID = @OrganizacionID
	  AND C.CorralID = @CorralID
	/*AND C.Capacidad >= L.CabezasInicio*/
	  AND C.Activo = 1
	  AND L.Activo = 1
	SET NOCOUNT OFF
END

GO
