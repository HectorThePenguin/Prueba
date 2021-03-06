USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Roque.Solis
-- Create date: 2014-03-20
-- Origen: APInterfaces
-- Description:	Obtiene un reparto por id
-- EXEC Reparto_ObtenerPorID 1, 4
--=============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerPorID]
	@RepartoID BIGINT,
	@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		RepartoID,
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
	FROM Reparto
	WHERE RepartoID = @RepartoID
	AND OrganizacionID = @OrganizacionID
	AND Activo = 1
	SET NOCOUNT OFF;
END

GO
