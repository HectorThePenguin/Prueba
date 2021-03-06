USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_ObtenerCorralesTipo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteTransferenciaGanado_ObtenerCorralesTipo]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_ObtenerCorralesTipo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Jesus.Alvarez
-- Create date: 2014-02-19
-- Origen: APInterfaces
-- Description:	Obtiene un listado de corrales por tipo.
-- EXEC CorteTransferenciaGanado_ObtenerCorralesTipo 4,5
--=============================================
CREATE PROCEDURE [dbo].[CorteTransferenciaGanado_ObtenerCorralesTipo]
	@OrganizacionID INT,
    @TipoCorralID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		C.CorralID,
		C.OrganizacionID,
		C.Codigo,
		C.TipoCorralID,
		C.Capacidad,
		C.MetrosLargo,
		C.MetrosAncho,
		C.Seccion,
		C.Orden,
		C.Activo,
		C.FechaCreacion,
		C.UsuarioCreacionID
	FROM Corral C (NOLOCK)
	WHERE C.OrganizacionID = @OrganizacionID
AND C.TipoCorralID = @TipoCorralID
AND C.Activo = 1
	SET NOCOUNT OFF;
END

GO
