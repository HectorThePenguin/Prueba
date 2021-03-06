USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerPartida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ObtenerPartida]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerPartida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 14/02/2014
-- Description:	Obtiene el numero de partida disponible.
-- [DeteccionGanado_ObtenerPartida] 4,1
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ObtenerPartida]
@OrganizacionID INT,
@CorralID INT
AS
BEGIN
	DECLARE @LoteID INT
	SET @LoteID = ISNULL((SELECT LoteID 
							FROM Lote 
							WHERE Activo = 1 
							AND CorralID = @CorralID AND OrganizacionID = @OrganizacionID),0)
	SELECT (ISNULL((
				SELECT FolioEntrada
				FROM EntradaGanado (NOLOCK) EG
				INNER JOIN Corral (NOLOCK) C
				ON (EG.CorralID = C.CorralID)
				INNER JOIN Lote (NOLOCK) L
				ON (C.CorralID = L.CorralID AND L.LoteID = EG.LoteID)
				WHERE C.OrganizacionID = @OrganizacionID AND C.CorralID = @CorralID 
							AND L.LoteID = @LoteID AND EG.Activo = 1 AND C.Activo = 1),0)) 
	AS NoPartida
	SET NOCOUNT OFF;
END

GO
