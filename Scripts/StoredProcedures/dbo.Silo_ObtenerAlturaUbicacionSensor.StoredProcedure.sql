USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Silo_ObtenerAlturaUbicacionSensor]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Silo_ObtenerAlturaUbicacionSensor]
GO
/****** Object:  StoredProcedure [dbo].[Silo_ObtenerAlturaUbicacionSensor]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Emir Lezama
-- Create date: 07/11/2014
-- Description: 
-- SpName     : exec Silo_ObtenerAlturaUbicacionSensor
--======================================================
CREATE PROCEDURE [dbo].[Silo_ObtenerAlturaUbicacionSensor]
@OrganizacionID INT
,@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT UbicacionSensor, AlturaSilo, OrdenSensor
	FROM MonitoreoSiloIndicador 
	WHERE OrganizacionID = @OrganizacionID
	AND Activo = @Activo
	ORDER BY OrdenSensor ASC
	SET NOCOUNT OFF;
END

GO
