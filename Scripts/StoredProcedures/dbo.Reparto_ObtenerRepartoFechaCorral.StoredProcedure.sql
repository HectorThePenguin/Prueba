USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoFechaCorral]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerRepartoFechaCorral]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoFechaCorral]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/03/21
-- Description: SP para consultar el reparto de la fecha actual
-- Origen     : APInterfaces
-- EXEC Reparto_ObtenerRepartoFechaCorral 4, 1, '2014-03-27'
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerRepartoFechaCorral]
@OrganizacionID INT,
@CorralID INT,
@Fecha DATETIME
AS
BEGIN
SET NOCOUNT ON;
	SELECT RepartoID,
		   OrganizacionID,
           CorralID,
           LoteID,
           Fecha,
		   PesoInicio,
		   PesoProyectado,
		   DiasEngorda,
		   PesoRepeso
	FROM Reparto
	WHERE OrganizacionID = @OrganizacionID
	AND CAST(Fecha AS DATE) = CAST(@Fecha AS DATE)
	AND CorralID = @CorralID
	AND Activo= 1;
	SET NOCOUNT OFF;
END

GO
