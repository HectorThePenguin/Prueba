USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerPorLote]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerPorLote]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerPorLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Roque.Solis
-- Create date: 2014-02-28
-- Origen: APInterfaces
-- Description:	Obtiene un reparto por lote
-- EXEC Reparto_ObtenerPorLote 1, 4, 1, '1900-01-01'
--=============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerPorLote]
	@LoteID INT,
	@OrganizacionID INT,
	@Activo INT,
	@Fecha DATETIME
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @FechaDefault date = '01-01-1900';
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
	WHERE LoteID = @LoteID
	AND OrganizacionID = @OrganizacionID
	AND Activo = @Activo
	AND CAST(Fecha AS DATE) = CASE WHEN CAST(@Fecha AS DATE) = @FechaDefault THEN CAST(Fecha AS DATE) ELSE CAST(@Fecha AS DATE) END
	SET NOCOUNT OFF;
END

GO
