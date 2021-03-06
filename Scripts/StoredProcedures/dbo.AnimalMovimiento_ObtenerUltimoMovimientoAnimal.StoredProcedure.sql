USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoAnimal]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Jesus Alvarez
-- Create date: 27/02/2014
-- Description: SP obtener el ultimo registro de AnimalMovimiento
-- Origen     : APInterfaces
-- EXEC [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoAnimal] 1, 4
-- =============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoAnimal]
	@AnimalID BIGINT,
	@OrganizacionID INT
AS
BEGIN
	SELECT TOP 1
		AnimalID,
		AnimalMovimientoID,
		OrganizacionID,
		CorralID,
		LoteID,
		FechaMovimiento,
		Peso,
		Temperatura,
		TipoMovimientoID,
		TrampaID,
		OperadorID,
		Observaciones,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM AnimalMovimiento (NOLOCK)
	WHERE AnimalID = @AnimalID
	AND OrganizacionID = @OrganizacionID
	AND Activo = 1
END

GO
