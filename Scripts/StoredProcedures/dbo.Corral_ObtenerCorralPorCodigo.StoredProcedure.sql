USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralPorCodigo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerCorralPorCodigo]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralPorCodigo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Cesar.Valdez
-- Create date: 2013-12-19
-- Origen: APInterfaces
-- Description:	Obtiene un Corral.
-- EXEC Corral_ObtenerCorralPorCodigo 'CRL02', 1, 7
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerCorralPorCodigo]
	@Codigo CHAR(10),
	@OrganizacionID INT,
    @TipoCorralID INT
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
	SET NOCOUNT OFF;
END

GO
