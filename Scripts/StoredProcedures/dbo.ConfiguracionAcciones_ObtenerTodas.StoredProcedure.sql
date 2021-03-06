USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAcciones_ObtenerTodas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionAcciones_ObtenerTodas]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAcciones_ObtenerTodas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Andres Vejar
-- Create date: 07/04/2014
-- Description: 
-- ConfiguracionAcciones_ObtenerTodas
--=============================================
CREATE PROCEDURE [dbo].[ConfiguracionAcciones_ObtenerTodas]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT AccionesSIAPId, Proceso, Descripcion, Codigo, FechaEjecucion,FechaUltimaEjecucion, Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo, Repetir, Activo,
		FechaCreacion, UsuarioCreacionId, FechaModificacion, UsuarioModificacionId
	FROM AccionesSIAP WHERE Activo = 1
	SET NOCOUNT OFF;
END

GO
