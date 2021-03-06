USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaRecuperacion_ObtenerCorralPorCodigo]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaRecuperacion_ObtenerCorralPorCodigo]
GO
/****** Object:  StoredProcedure [dbo].[SalidaRecuperacion_ObtenerCorralPorCodigo]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Edgar.Villarreal
-- Create date: 05-03-2014
-- Origen: APInterfaces
-- Description:	Obtiene un Corral con tipo corral y grupo.
-- EXEC SalidaRecuperacion_ObtenerCorralPorCodigo '002', 4, 9,4
--=============================================
CREATE PROCEDURE [dbo].[SalidaRecuperacion_ObtenerCorralPorCodigo]
	@Codigo CHAR(10),
	@OrganizacionID INT,
	@TipoCorralID INT,
	@GrupoCorralID INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1
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
		C.UsuarioCreacionID,
		TC.GrupoCorralID
	FROM Corral C
	INNER JOIN TipoCorral AS TC ON TC.TipoCorralID = C.TipoCorralID
	WHERE Codigo = @Codigo
	AND C.OrganizacionID = @OrganizacionID
	AND C.TipoCorralID = @TipoCorralID
	AND TC.GrupoCorralID = @GrupoCorralID
	SET NOCOUNT OFF;
END

GO
