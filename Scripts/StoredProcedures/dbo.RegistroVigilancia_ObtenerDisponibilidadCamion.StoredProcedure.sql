USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilancia_ObtenerDisponibilidadCamion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RegistroVigilancia_ObtenerDisponibilidadCamion]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilancia_ObtenerDisponibilidadCamion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=================================================================
-- Author     : Raul Vega
-- Create date: 18/06/2015 
-- Origen     : AP Interfaces
-- Description: Verifica si un camion por placa ya salio
-- RegistroVigilancia_ObtenerDisponibilidadCamion ('AAAAA')
--=================================================================
CREATE PROCEDURE [dbo].[RegistroVigilancia_ObtenerDisponibilidadCamion]
	@Camion VARCHAR(10)
AS
BEGIN
	SELECT COUNT(1) AS SalidaCamion
	FROM RegistroVigilancia (NOLOCK)
	WHERE Camion=@Camion
	AND Activo = 1
	AND FechaSalida IS NULL
END

GO
