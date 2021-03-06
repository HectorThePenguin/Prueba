USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaRecuperacion_ObtenerCorralEnfermeria]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaRecuperacion_ObtenerCorralEnfermeria]
GO
/****** Object:  StoredProcedure [dbo].[SalidaRecuperacion_ObtenerCorralEnfermeria]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: EDGAR.VILLARREAL
-- Fecha: 12/02/2014
-- Origen: APInterfaces
-- Descripci�n:	Obtiene los aretes para el corral de enfermeria especificado
-- EXEC TraspasoGanado_ObtenerAretesCorral '011', 4, 3,1,8
-- EXEC SalidaRecuperacion_ObtenerCorralEnfermeria '003', 4, 3,1,8
-- =============================================
CREATE PROCEDURE [dbo].[SalidaRecuperacion_ObtenerCorralEnfermeria]
@Codigo CHAR(10),
@OrganizacionID INT,
@GrupoCorralID INT,
@Activo INT
AS
BEGIN
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
	INNER JOIN TipoCorral TC ON TC.TipoCorralID = C.TipoCorralID
	WHERE C.Codigo = @Codigo
	AND C.OrganizacionID = @OrganizacionID
	AND C.Activo = @Activo
	AND TC.GrupoCorralID = @GrupoCorralID
END

GO
