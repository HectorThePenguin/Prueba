USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerExistenciaCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerExistenciaCorral]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerExistenciaCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Jesus.Alvarez
-- Create date: 2014-02-24
-- Origen: APInterfaces
-- Description:	Obtiene un Corral.
-- EXEC Corral_ObtenerExistenciaCorral 'CRL02', 1
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerExistenciaCorral]
	@Codigo CHAR(10),
	@OrganizacionID INT
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
		C.UsuarioCreacionID
	FROM Corral C
	WHERE Codigo = @Codigo
	AND C.OrganizacionID = @OrganizacionID
	AND C.Activo = 1
	SET NOCOUNT OFF;
END

GO
