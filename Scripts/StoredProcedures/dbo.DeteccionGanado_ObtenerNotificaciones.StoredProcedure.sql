USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerNotificaciones]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ObtenerNotificaciones]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerNotificaciones]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor: Pedro.Delgado
-- Fecha: 12/02/2014
-- Origen: APInterfaces
-- Descripción:	Obtiene las notificaciones del operador
-- EXEC DeteccionGanado_ObtenerNotificaciones 4, 7
-- =============================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ObtenerNotificaciones] @OrganizacionID INT
	,@OperadorID INT
AS
BEGIN
	SELECT SG.SupervisionGanadoID
		,C.Codigo
		,CD.Descripcion
		,SG.Acuerdo
		,SG.Arete
		,SG.AreteMetalico AS AreteTestigo
		,SG.FotoSupervision
		,sg.Notificacion
	FROM SupervisionGanado(NOLOCK) SG
	INNER JOIN ConceptoDeteccion(NOLOCK) CD ON (SG.ConceptoDeteccionID = CD.ConceptoDeteccionID)
	INNER JOIN Lote L ON (SG.LoteID = L.LoteID)
	INNER JOIN Corral C ON (L.CorralID = C.CorralID)
	INNER JOIN CorralDetector CDT ON (C.CorralID = CDT.CorralID)
	WHERE SG.Notificacion = 1
		AND C.OrganizacionID = @OrganizacionID
		AND CDT.OperadorID = @OperadorID
		AND CDT.Activo = 1
END

GO
