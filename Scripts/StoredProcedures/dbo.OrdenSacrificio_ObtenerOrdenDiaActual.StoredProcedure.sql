USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_ObtenerOrdenDiaActual]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[OrdenSacrificio_ObtenerOrdenDiaActual]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_ObtenerOrdenDiaActual]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/02/28
-- Description: SP para obtener la orden de sacrificio del dia actual
-- Origen     : APInterfaces
-- EXEC OrdenSacrificio_ObtenerOrdenDiaActual 4,1
-- =============================================
CREATE PROCEDURE [dbo].[OrdenSacrificio_ObtenerOrdenDiaActual]
	@OrganizacionID INT,
	@FechaOrden SMALLDATETIME,
	@EstatusID INT,
	@Activo INT
AS
BEGIN
		SELECT 
			TOP 1
			OrdenSacrificioID,
			FolioOrdenSacrificio,
			OrganizacionID,
			Observaciones,
			EstatusID,
			Activo
		FROM OrdenSacrificio 
		WHERE OrganizacionID = @OrganizacionID
		AND EstatusID = @EstatusID
		AND Activo= @Activo
		AND cast(convert(char(8),FechaOrden, 112) as smalldatetime) = cast(convert(char(8), @FechaOrden, 112) as smalldatetime);
END

GO
