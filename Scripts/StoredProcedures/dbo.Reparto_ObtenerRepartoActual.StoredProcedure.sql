USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoActual]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerRepartoActual]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoActual]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/03/21
-- Description: SP para consultar el reparto de la fecha actual
-- Origen     : APInterfaces
-- EXEC Reparto_ObtenerRepartoActual 4
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerRepartoActual]
@OrganizacionID INT
AS
BEGIN
SET NOCOUNT ON;
	SELECT RepartoID,
		   OrganizacionID,
           LoteID,
           Fecha,
		   PesoInicio,
		   PesoProyectado,
		   DiasEngorda,
		   PesoRepeso
	FROM Reparto
	WHERE OrganizacionID = @OrganizacionID
	AND CAST(Fecha AS DATE) = CAST(GETDATE() AS DATE)
	AND Activo= 1;
	SET NOCOUNT OFF;
END

GO
