USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerPorRepartosID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerPorRepartosID]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerPorRepartosID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Roque.Solis
-- Create date: 21/10/2014
-- Description:	Obtiene un reparto por id
-- EXEC Reparto_ObtenerPorRepartosID 1, 4
--=============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerPorRepartosID]
	@RepartoXML XML,
	@OrganizacionID INT
AS
CREATE TABLE #RepartosID
(
	RepartoID BIGINT
)
INSERT #RepartosID (RepartoID)
	SELECT RepartoID = t.item.value('./RepartoID[1]', 'BIGINT')		
	FROM @RepartoXML.nodes('ROOT/Reparto') AS T(item)
BEGIN
	SET NOCOUNT ON;
	SELECT
		re.RepartoID,
		OrganizacionID,
		CorralID,
		LoteID,
		Fecha,
		PesoInicio,
		PesoProyectado,
		DiasEngorda,
		PesoRepeso,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM Reparto re
	inner join #RepartosID rid on re.RepartoID = rid.RepartoID
	WHERE OrganizacionID = @OrganizacionID
	AND Activo = 1
	SET NOCOUNT OFF;
END

GO
