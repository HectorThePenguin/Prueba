USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAcciones_ObtenerPorCodigo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionAcciones_ObtenerPorCodigo]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAcciones_ObtenerPorCodigo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Andres Vejar
-- Create date: 07/04/2014
-- Description: 
-- ConfiguracionAcciones_ObtenerPorCodigo
--=============================================
CREATE PROCEDURE [dbo].[ConfiguracionAcciones_ObtenerPorCodigo]
@Codigo varchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT AccionesSIAPId, Proceso, Descripcion, Codigo, FechaEjecucion, Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo, Repetir, Activo,
		FechaCreacion, UsuarioCreacionId, FechaModificacion, UsuarioModificacionId
	FROM AccionesSIAP WHERE Codigo = @Codigo AND Activo = 1
	SET NOCOUNT OFF;
END

GO
