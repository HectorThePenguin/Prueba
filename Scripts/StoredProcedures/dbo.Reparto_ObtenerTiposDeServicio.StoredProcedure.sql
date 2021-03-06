USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerTiposDeServicio]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerTiposDeServicio]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerTiposDeServicio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/03/21
-- Description: SP para consultar los tipos de servicios
-- Origen     : APInterfaces
-- EXEC Reparto_ObtenerTiposDeServicio
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerTiposDeServicio]
AS
BEGIN
	SELECT TipoServicioID,
			HoraInicio,
			HoraFin,
			Descripcion
	FROM TipoServicio
	WHERE Activo = 1
END

GO
